using Demo.BusinessLogic.DTOS.EmployeeDTOS;
using Demo.DataAccess.Models.EmployeeModule;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        // Define method signatures for employee-related operations
        // GetAll
        IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking = false);
        // GetById
        EmployeeDetailsDto? GetEmployeeById(int id);
        //Create Employee
        int CreateEmployee(CreateEmployeeDto employeeDto);
        // Update Employee
        int UpdateEmployee(UpdatedEmployeeDto updatedEmployee);
        // Delete Employee
        bool DeleteEmployee(int id);

    }
}
