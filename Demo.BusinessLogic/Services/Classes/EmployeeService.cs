using Demo.BusinessLogic.DTOS.EmployeeDTOS;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Repositories.Interfaces;


namespace Demo.BusinessLogic.Services.Classes
{
    internal class EmployeeService(IEmployeeRepository _employeeRepository) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking = false)
        {
            var employees = _employeeRepository.GetAll(withTracking = false);
            return employees.Select(e => new EmployeeDto()
            {
                // Manually map properties from Employee to EmployeeDto
                Id = e.Id,
                Name = e.Name,
                Age = e.Age,
                Salary = e.Salary,
                IsActive = e.IsActive,
                Email = e.Email,
                Gender = e.Gender.ToString(),
                EmployeeType = e.EmployeeType.ToString()
            });
            
        }
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee == null ? null : new EmployeeDetailsDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                Address = employee.Address,
                Gender = employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),
                PhoneNumber = employee.PhoneNumber,
                HiringDate = DateOnly.FromDateTime(employee.HiringDate),
                CreatedBy = employee.CreatedBy,
                CreatedOn = employee.CreatedAt,
                ModifiedBy = employee.ModifiedBy,
                ModifiedOn = employee.ModifiedAt
            };
        }
        public int CreateEmployee(CreateEmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }



        public int UpdateEmployee(UpdatedEmployeeDto updatedEmployee)
        {
            throw new NotImplementedException();
        }
    }
}
