using AutoMapper;
using Demo.BusinessLogic.DTOS.EmployeeDTOS;
using Demo.BusinessLogic.Services.Attachment_Service;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.EmployeeModule;
using System;


namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper,IAttachmentService _attachmentService) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName, bool withTracking = false)
        {
        var empRepo = _unitOfWork.EmployeeRepository;
            IEnumerable<Employee> employees;
            if (!string.IsNullOrEmpty(EmployeeSearchName))
            {
                employees = empRepo.GetAll(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            }
            else
                employees = empRepo.GetAll(withTracking);

            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

        }
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            return employee == null ? null : _mapper.Map<EmployeeDetailsDto>(employee);
        }
        public int CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            // Set the audit fields
            employee.CreatedById = employeeDto.CreatedById;
            employee.CreatedAt = DateTime.UtcNow;

            // check for duplicate email
            var existingEmails = _unitOfWork.EmployeeRepository.GetAll(e => e.Email);
            if (existingEmails.Any(email => email.Equals(employeeDto.Email, StringComparison.OrdinalIgnoreCase)))
                return -1;

            // handle image upload
            if (employeeDto.Image is not null)
            {
                string? imgName = _attachmentService.Upload(employeeDto.Image, "Images");
                employee.ImageName = imgName;
            }

            _unitOfWork.EmployeeRepository.Add(employee);
            return _unitOfWork.SaveChanges();
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto, string? modifiedById)
        {
            var existingEmployee = _unitOfWork.EmployeeRepository.GetById(employeeDto.id);
            if (existingEmployee is null)
                return 0;

            if (employeeDto.Image is not null)
            {
                // Delete old image if exists
                if (!string.IsNullOrEmpty(existingEmployee.ImageName))
                {
                    string oldPath = Path.Combine("wwwroot", "Images", existingEmployee.ImageName);
                    _attachmentService.Delete(oldPath);
                }

                // Upload new image
                string imgName = _attachmentService.Upload(employeeDto.Image, "Images");
                existingEmployee.ImageName = imgName;
            }

            existingEmployee.Name = employeeDto.Name;
            existingEmployee.Email = employeeDto.Email;
            existingEmployee.PhoneNumber = employeeDto.PhoneNumber;
            existingEmployee.DepartmentId = employeeDto.DepartmentId;

            existingEmployee.ModifiedAt = DateTime.Now;
            existingEmployee.ModifiedById = modifiedById;  // Set modifier user

            _unitOfWork.EmployeeRepository.Update(existingEmployee);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                 _unitOfWork.EmployeeRepository.Update(employee) ;
                return _unitOfWork.SaveChanges() > 0;
            }
        }


    }
}
