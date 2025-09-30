using Demo.BusinessLogic.DTOS.EmployeeDTOS;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.Shared;
using Demo.Presentaion.ViewModels;
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
        public IActionResult Create() // to avoid over-injecting in the constructor
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeemodel)
        {
            if (!ModelState.IsValid)
                return View(employeemodel);
            try
            {

                int result = _employeeService.CreateEmployee(new CreateEmployeeDto()

                {
                    Name = employeemodel.Name,
                    Age = employeemodel.Age,
                    Address = employeemodel.Address,
                    Salary = employeemodel.Salary,
                    IsActive = employeemodel.IsActive,
                    DepartmentId = employeemodel.DepartmentId,
                    Email = employeemodel.Email,
                    PhoneNumber = employeemodel.PhoneNumber,
                    HiringDate = employeemodel.HiringDate,
                    EmployeeType = Enum.Parse<EmployeeType>(employeemodel.EmployeeType.ToString()),
                    Gender = employeemodel.Gender

                });

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "The employee could not be created. Please try again.");
                return View(employeemodel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating employee");

                if (_env.IsDevelopment())
                {
                    // Show detailed error message in Dev
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeemodel);
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
            if(emp is null) return NotFound();
            var employeeViewModel = new EmployeeViewModel() // DepartmentId
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
                Gender = Enum.Parse<Gender>(emp.Gender),
                                    

            };
            return View(employeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
        {
            if (!id.HasValue || id.Value != employeeViewModel.id) return BadRequest();
            if(!ModelState.IsValid) return View(employeeViewModel);
            try
            {
                int result = _employeeService.UpdateEmployee(new UpdatedEmployeeDto()
                {
                    id = id.Value, // efcore check if the entity already exists (takes the id from the route)
                    Name = employeeViewModel.Name,
                    Age = employeeViewModel.Age,
                    Address = employeeViewModel.Address,
                    Salary = employeeViewModel.Salary,
                    IsActive = employeeViewModel.IsActive,
                    DepartmentId = employeeViewModel.DepartmentId,
                    Email = employeeViewModel.Email,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    HiringDate = employeeViewModel.HiringDate,
                    EmployeeType = Enum.Parse<EmployeeType>(employeeViewModel.EmployeeType.ToString()),
                    Gender = employeeViewModel.Gender,

                });
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
            return View(employeeViewModel);
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
