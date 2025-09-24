using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentaion.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService) : Controller
    {
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }
    }
}
