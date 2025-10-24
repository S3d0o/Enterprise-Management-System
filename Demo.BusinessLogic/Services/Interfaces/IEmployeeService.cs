using Demo.BusinessLogic.DTOS.EmployeeDTOS;
using Demo.BusinessLogic.Services.Classes;
using Demo.DataAccess.Models.EmployeeModule;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        // Define method signatures for employee-related operations
        // GetAll
        IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName,bool withTracking = false);
        // GetById
        EmployeeDetailsDto? GetEmployeeById(int id);
        //Create Employee
        ResultService CreateEmployee(CreateEmployeeDto employeeDto);
        // Update Employee
        ResultService UpdateEmployee(UpdatedEmployeeDto updatedEmployee,string? modifiedById);
        // Delete Employee
        bool DeleteEmployee(int id);

    }
}
