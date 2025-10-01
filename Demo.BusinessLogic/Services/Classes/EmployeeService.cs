using AutoMapper;
using Demo.BusinessLogic.DTOS.EmployeeDTOS;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Models.EmployeeModule;


namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository, IMapper _mapper) : IEmployeeService
    {

        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName, bool withTracking = false)
        {
            IEnumerable<Employee> employees;
            if (!string.IsNullOrEmpty(EmployeeSearchName))
            {
                employees = _employeeRepository.GetAll(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            }
            else
                employees = _employeeRepository.GetAll(withTracking);

            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
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
            return _employeeRepository.Add(_mapper.Map<CreateEmployeeDto, Employee>(employeeDto));
        }
        public int UpdateEmployee(UpdatedEmployeeDto updatedEmployee)
        {
            return _employeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(updatedEmployee));
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee) > 0 ? true : false;
            }
        }

    }
}
