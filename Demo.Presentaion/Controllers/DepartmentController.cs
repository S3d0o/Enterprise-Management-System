using Demo.BusinessLogic.DTOS.DepartmentDTOS;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.Presentaion.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentaion.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,
        IWebHostEnvironment _env, ILogger<DepartmentController> _logger) : Controller
    {
        // Base URL/Controller/Action  
        public ActionResult Index()
        {
            //ViewData["message"] = "Hello from ViewData";
            //ViewBag.message = "Hello from ViewBag";
            ViewData["message"] = new DepartmentDto() { DeptName = "TEST" }; // safe, casting is needed (checking in compile time)
            ViewBag.message = new DepartmentDto() { DeptName = "test" };// unsafe, no need for casting (checking in run time)
            var departments = _departmentService.GetAllDepartments();
            return View(departments); // model here is departments(Data)
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid) // server side validation 
            {
                try
                {
                    int result = _departmentService.AddDepartment(new CreateDepartmentDto()
                    {
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        Description = departmentViewModel.Description,
                        CreatedAt = departmentViewModel.CreatedAt
                    });
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Something went wrong, Department cannot be created");
                    }
                }
                catch (Exception ex)
                {
                    //Development => action => log error in Console , view , message box ,...
                    // Development => log error in the console , return the same view with the model to let the user try again
                    if (_env.IsDevelopment())
                    {
                        _logger.LogError(ex, ex.Message); // log error in the console
                    }
                    //Production(Deployment) => another action => log error in a file or database, return view (Error)
                    // Production => log error in a file or database, return view (Error)
                    else
                    {
                        _logger.LogError(ex, "An unexpected error occurred while creating department");

                        // Show user-friendly error page 
                        return View("ErrorView");
                    }
                }
            }

            return View(departmentViewModel); // to show the validation errors

        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest(); // 400 
            var DepartmentDetails = _departmentService.GetDepartmentById(id.Value);
            if (DepartmentDetails is null) return NotFound(); // 404
            return View(DepartmentDetails);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var updatedDepartmentDto = new DepartmentViewModel()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedAt = department.CreatedAt.HasValue ? department.CreatedAt.Value : default
            };
            return View(updatedDepartmentDto);
            //return View(department);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid) return View(departmentVM);
            try
            {
                if (!id.HasValue) return BadRequest();
                var UpdatedDepartmentDto = new UpdatedDepartmentDto() // manual mapping
                {
                    Id = id.Value,
                    Name = departmentVM.Name,
                    Code = departmentVM.Code,
                    Description = departmentVM.Description,
                    CreatedAt = departmentVM.CreatedAt
                };

                int result = _departmentService.UpdateDepartment(UpdatedDepartmentDto);
                if (result > 0) return RedirectToAction("Index");
                ModelState.AddModelError(string.Empty, "Something went wrong, Department cannot be updated"); // if update failed 
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    _logger.LogError($"the department cannot be updated because{ex.Message}");
                else
                {
                    _logger.LogError(ex, "An unexpected error occurred while updating department");
                    return View("ErrorView", ex); // return the same view with the model to let the user try again
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
           
                if (!id.HasValue) return BadRequest();
                var department = _departmentService.GetDepartmentById(id.Value);
                if (department is null) return NotFound();
                return View(department);
        }

        [HttpPost]
        public IActionResult Delete([FromRoute] int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool IsDeleted = _departmentService.DeleteDepartment(id);
                if (IsDeleted) return RedirectToAction("Index");
                else
                    ModelState.AddModelError(string.Empty, "Something went wrong, Department cannot be deleted");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    _logger.LogError($"the department cannot be deleted because{ex.Message}");
                else // Production environment
                {
                    _logger.LogError(ex, "An unexpected error occurred while deleting department");
                    return View("ErrorView", ex); // return the same view with the model to let the user try again
                }
            }
            return RedirectToAction(nameof(Delete), new { id });

        }

       
    }

}
