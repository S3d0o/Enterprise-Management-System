using AutoMapper;
using Demo.BusinessLogic.DTOS.EmployeeDTOS;
using Demo.BusinessLogic.Services.Attachment_Service;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.EmployeeModule;


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
            //    new EmployeeDetailsDto()
            //{
            //    Id = employee.Id,
            //    Name = employee.Name,
            //    Age = employee.Age,
            //    Salary = employee.Salary,
            //    IsActive = employee.IsActive,
            //    Email = employee.Email,
            //    Address = employee.Address,
            //    Gender = employee.Gender.ToString(),
            //    EmployeeType = employee.EmployeeType.ToString(),
            //    PhoneNumber = employee.PhoneNumber,
            //    HiringDate = DateOnly.FromDateTime(employee.HiringDate),
            //    CreatedBy = employee.CreatedBy,
            //    CreatedOn = employee.CreatedAt,
            //    ModifiedBy = employee.ModifiedBy,
            //    ModifiedOn = employee.ModifiedAt
            //};
        }
        public int CreateEmployee(CreateEmployeeDto employeeDto)
        {
             _unitOfWork.EmployeeRepository.Add(_mapper.Map<CreateEmployeeDto, Employee>(employeeDto));
            return _unitOfWork.SaveChanges();
        }   
        public int UpdateEmployee(UpdatedEmployeeDto updatedEmployee)
        {
            _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(updatedEmployee));
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
