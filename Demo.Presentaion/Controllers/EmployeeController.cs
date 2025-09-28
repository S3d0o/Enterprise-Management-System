using Demo.BusinessLogic.DTOS.EmployeeDTOS;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

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

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var emp = _employeeService.GetEmployeeById(id!.Value);
            if(emp is null) return NotFound();
            return View(emp);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var emp = _employeeService.GetEmployeeById(id!.Value);
            var employee = _employeeService.GetEmployeeById(id.Value);
            if(employee is null) return NotFound();
            var employeeDto = new UpdatedEmployeeDto()
            {
                id = emp!.Id,
                Name = emp.Name,
                Age = emp.Age,
                Address = emp.Address,
                Salary = emp.Salary,
                IsActive = emp.IsActive,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                HiringDate = emp.HiringDate,
                EmployeeType = Enum.Parse<EmployeeType>(emp.EmployeeType),
                Gender = Enum.Parse<Gender>(emp.Gender)
            };
            return View(employeeDto);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, UpdatedEmployeeDto employeeDto)
        {
            if (!id.HasValue || id.Value != employeeDto.id) return BadRequest();
            if(!ModelState.IsValid) return View(employeeDto);
            try
            {
                int result = _employeeService.UpdateEmployee(employeeDto);
                if(result>0)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError(string.Empty, "The employee could not be updated. Please try again.");
            }
            catch (Exception ex)
            { 
                if(_env.IsDevelopment())
                    _logger.LogError(ex, "Error while updating employee");
                else
                    ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(employeeDto);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id==0) return BadRequest();
            try
            {
                bool IsDeleted = _employeeService.DeleteEmployee(id);
                if (IsDeleted) return RedirectToAction("Index");
                else
                    ModelState.AddModelError(string.Empty, "Something went wrong, Employee cannot be deleted");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    _logger.LogError($"the department cannot be deleted because{ex.Message}");
                else // Production environment
                {
                    _logger.LogError(ex, "An unexpected error occurred while deleting Employee");
                    return View("ErrorView", ex); // return the same view with the model to let the user try again
                }
            }
            return RedirectToAction(nameof(Delete), new { id });

        }
    }
}
