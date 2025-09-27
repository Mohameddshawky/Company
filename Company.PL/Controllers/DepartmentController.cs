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
        public IActionResult Index(string? DepartmentSearchName)
        {
            IEnumerable<Departments> departments;
            if(string.IsNullOrEmpty(DepartmentSearchName))  
                departments=departmentRepository.GetAll().ToList();
            else
                departments = departmentRepository.Search(DepartmentSearchName).ToList();

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
                string message;
                if (cnt > 0) {
                     message = "Department Created Successfully";
                }
                else
                {
                    message = "Department Can Not Be Created ";
                }
                TempData["message"] = message;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(int?id,string ViewName= "Details")
        {
            if (id is null) return BadRequest();

            var department = departmentRepository.Get(id.Value);

            if (department is null) return NotFound(new {StatusCode=404,Message="Department is not found"});

            return View(ViewName,department);    
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id is null) return BadRequest();

            var department = departmentRepository.Get(id.Value);

            if (department is null) return NotFound(new { StatusCode = 404, Message = "Department is not found" });
            var Department = new CreateDepartmentDto
            {
                Name = department.Name,
                Code = department.Code,
                CreateAt = department.CreateAt,
            };

            return View(Department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//prefer for any post action
        //prevent any one to request rather than client side
        public IActionResult Edit([FromRoute] int id, Departments model)
        {
            if (ModelState.IsValid)
            {
                var Department = new Departments
                {
                    Id = id,
                    Name = model.Name,
                    Code = model.Code,
                    CreateAt = model.CreateAt,
                };
                var cnt = departmentRepository.Update(Department);
                if (cnt > 0) return RedirectToAction(nameof(Index));


            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
           
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Departments department)
        {
            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    var cnt = departmentRepository.Delete(department);
                    if (cnt > 0) return RedirectToAction(nameof(Index));
                }
                else
                    return BadRequest();
            }
            return View(department);
        }
    }
}
