using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    //mvc controller
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentController(IDepartmentRepository _departmentRepository)
        {
            departmentRepository = _departmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
           
            var departments=departmentRepository.GetAll().ToList();
            return View(departments);
        }
    }
}
