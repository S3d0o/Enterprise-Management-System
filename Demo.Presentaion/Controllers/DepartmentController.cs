using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentaion.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService) : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        
        
    }
}
