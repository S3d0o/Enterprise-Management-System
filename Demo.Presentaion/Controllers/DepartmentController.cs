using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentaion.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService) : Controller
    {
        // Base URL/Controller/Action  
        public ActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments); // model here is departments(Data)
        }

        
        
    }
}
