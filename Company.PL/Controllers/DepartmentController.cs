using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.DTos;
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) {//server side validation
                var Department = new Departments
                {
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt,
                };
               var cnt= departmentRepository.Add(Department);
                if (cnt > 0) {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
    }
}
