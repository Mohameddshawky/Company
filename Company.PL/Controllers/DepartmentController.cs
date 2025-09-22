using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.DTos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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
        [HttpGet]
        public IActionResult Details(int?id)
        {
            if (id is null) return BadRequest();

            var department = departmentRepository.Get(id.Value);

            if (department is null) return NotFound(new {StatusCode=404,Message="Department is not found"});

            return View(department);    
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();

            var department = departmentRepository.Get(id.Value);

            if (department is null) return NotFound(new { StatusCode = 404, Message = "Department is not found" });

            return View(department);    
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id,Departments department)
        {
            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    var cnt = departmentRepository.Update(department);
                    if (cnt > 0) return RedirectToAction(nameof(Index));
                }
                else
                    return BadRequest();
            }
            return View(department);
        }
    }
}
