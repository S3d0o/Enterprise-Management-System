using Demo.BusinessLogic.DTOS.DepartmentDTOS;
using Demo.BusinessLogic.DTOS.EmployeeDTOS;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModule;
using Demo.DataAccess.Models.Shared;
using Demo.Presentaion.ViewModels.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;
using System.Security.Claims;

namespace Demo.Presentaion.Controllers
{
    [Authorize]
    public class EmployeeController(IEmployeeService _employeeService,
        IWebHostEnvironment _env, ILogger<EmployeeController> _logger) : Controller
    {
        [HttpGet]
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        }


        [HttpGet]
        public IActionResult Create([FromServices] IDepartmentService _departmentService)
        {
            var departments = _departmentService.GetAllDepartments();

            var model = new EmployeeViewModel
            {
                DepartmentList = new SelectList(_departmentService.GetAllDepartments(),
                                        nameof(DepartmentDto.DeptId),
                                        nameof(DepartmentDto.DeptName))
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(EmployeeViewModel employeemodel, [FromServices] IDepartmentService _departmentService)
        {
            if (!ModelState.IsValid)
            {
                employeemodel.DepartmentList = new SelectList(_departmentService.GetAllDepartments(),
                                              nameof(DepartmentDto.DeptId),
                                              nameof(DepartmentDto.DeptName));
                return View(employeemodel);
            }

            try
            {
                // Get the logged-in user ID
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var employeeDto = new CreateEmployeeDto
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
                    EmployeeType = employeemodel.EmployeeType,
                    Gender = employeemodel.Gender,
                    Image = employeemodel.Image,

                    // Pass the creator ID
                    CreatedById = userId
                };

                ResultService result = _employeeService.CreateEmployee(employeeDto);

                if (result.Success)
                {
                    TempData["message"] = $" Employee '{employeemodel.Name}' was created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["message"] = " Something went wrong. Employee could not be created."; // we could used result.Message here if needed
                ModelState.AddModelError(string.Empty, result.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating employee");

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                else
                    ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please contact support.");
            }

            employeemodel.DepartmentList = new SelectList(_departmentService.GetAllDepartments(),
                                      nameof(DepartmentDto.DeptId),
                                      nameof(DepartmentDto.DeptName));
            return View(employeemodel);
        }


        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var emp = _employeeService.GetEmployeeById(id!.Value);
            if (emp is null) return NotFound();
            return View(emp);
        }

        [HttpGet]
        public IActionResult Edit(int? id, [FromServices] IDepartmentService _departmentService)
        {
            if (!id.HasValue) return BadRequest();
            var emp = _employeeService.GetEmployeeById(id!.Value);
            if (emp is null) return NotFound();
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
                DepartmentId = emp.DepartmentId,
                ImageName = emp.ImageName,
                DepartmentList = new SelectList(
            _departmentService.GetAllDepartments(),
            nameof(DepartmentDto.DeptId),
            nameof(DepartmentDto.DeptName),
            emp.DepartmentId),// <-- selected department


            };
            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
        {
            if (!id.HasValue || id.Value != employeeViewModel.id) return BadRequest();
            if (!ModelState.IsValid) return View(employeeViewModel);
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in user ID

                ResultService result = _employeeService.UpdateEmployee(new UpdatedEmployeeDto()
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
                    Image = employeeViewModel.Image,
                    ModifiedById = userId // Pass the modifier ID

                },userId);
                if (result.Success)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError(string.Empty, result.Message);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    _logger.LogError(ex, "Error while updating employee");
                else
                    ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
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
