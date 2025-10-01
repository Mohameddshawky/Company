using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.DTos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    //mvc controller
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? DepartmentSearchName)
        {
            IEnumerable<Departments> departments;
            if (string.IsNullOrEmpty(DepartmentSearchName))
                departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            else
                departments = await unitOfWork.DepartmentRepository.SearchAsync(DepartmentSearchName);

            return View(departments);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) {//server side validation
                
                var Department = mapper.Map<Departments>(model);
                await unitOfWork.DepartmentRepository.AddAsync(Department);
                var cnt=await unitOfWork.SaveChangesAsync();
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
        public async Task<IActionResult> Details(int?id,string ViewName= "Details")
        {
            if (id is null) return BadRequest();

            var department = await unitOfWork.DepartmentRepository.GetAsync(id.Value);

            if (department is null) return NotFound(new {StatusCode=404,Message="Department is not found"});

            return View(ViewName,department);    
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id is null) return BadRequest();

            var department = await unitOfWork.DepartmentRepository.GetAsync(id.Value);

            if (department is null) return NotFound(new { StatusCode = 404, Message = "Department is not found" });
            
            var Department = mapper.Map<CreateDepartmentDto>(department);

            return View(Department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//prefer for any post action
        //prevent any one to request rather than client side
        public async Task<IActionResult> Edit([FromRoute] int id, Departments model)
        {
            if (ModelState.IsValid)
            {
                
                
                var Department = mapper.Map<Departments>(model);
                Department.Id = id; 
                unitOfWork.DepartmentRepository.Update(Department);
                var cnt= await unitOfWork.SaveChangesAsync();   
                if (cnt > 0) return RedirectToAction(nameof(Index));


            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
           
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Departments department)
        {
            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    unitOfWork.DepartmentRepository.Delete(department);
                    var cnt = await unitOfWork.SaveChangesAsync();
                    if (cnt > 0) return RedirectToAction(nameof(Index));
                }
                else
                    return BadRequest();
            }
            return View(department);
        }
    }
}
