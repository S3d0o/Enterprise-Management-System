using Demo.BusinessLogic.DTOS.EmployeeDTOS;
using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentaion.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService,
        IWebHostEnvironment _env, ILogger<EmployeeController> _logger) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }
       
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            try
            {
                int result = _employeeService.CreateEmployee(employee);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "The employee could not be created. Please try again.");
                return View(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating employee");

                if (_env.IsDevelopment())
                {
                    // Show detailed error message in Dev
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employee);
                }

                // Show generic error in Prod
                return View("ErrorView");
            }
        }

    }
}
