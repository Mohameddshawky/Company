using AutoMapper;
using Company.BLL.AttachmentService;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.DTos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAttachmentService attachmentService;

        public EmployeeController(
           IUnitOfWork unitOfWork,
           IMapper mapper,
            IAttachmentService attachmentService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.attachmentService = attachmentService;
        }
        [HttpGet]
        public IActionResult Index(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees;
            if (String.IsNullOrEmpty(EmployeeSearchName))
                 employees = unitOfWork.EmployeeRepository.GetAll().ToList();
            else
                employees= unitOfWork.EmployeeRepository.Search(EmployeeSearchName);
                return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeDTO model)
        {  
            var employee = mapper.Map<Employee>(model);
            if(model.Image is not null)
            {
               string? FileName= attachmentService.Upload(model.Image, "images");
                employee.ImageName = FileName;
            }
            if (ModelState.IsValid)
            {
             
                unitOfWork.EmployeeRepository.Add(employee);
                var cnt=unitOfWork.SaveChanges();
                string message;
                if (cnt > 0)
                {
                    message = "Employee Created Successfully";
                }
                else {
                    message = "Employee Can Not Be Created ";

                }
                TempData["message"]=message;
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var employee = unitOfWork.EmployeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, Message = "Employee is not found" });

            return View(ViewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id is null) return BadRequest();


            var model = unitOfWork.EmployeeRepository.Get(id.Value);

            if (model is null) return NotFound(new { StatusCode = 404, Message = "Employee is not found" });
           
            var employee = mapper.Map<EmployeeDTO>(model);


            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//prefer for any post action
        //prevent any one to request rather than client side
        public IActionResult Edit([FromRoute] int id, EmployeeDTO model)
        {
            
            if (ModelState.IsValid)
            {
                if (model.ImageName is not null && model.Image is not null)
                {
                    var ch=attachmentService.Delete(model.ImageName);

                }
                if(model.Image is not null)
                {
                  model.ImageName=  attachmentService.Upload(model.Image,"images");
                }
                
                var employee = mapper.Map<Employee>(model);
                employee.Id = id;
         


                unitOfWork.EmployeeRepository.Update(employee);
                var cnt= unitOfWork.SaveChanges();
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
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id == employee.Id)
                {
                     unitOfWork.EmployeeRepository.Delete(employee);
                    var cnt=unitOfWork.SaveChanges();
                    if (cnt > 0) {
                        attachmentService.Delete(employee.ImageName);
                        return RedirectToAction(nameof(Index)); }
                }
                else
                    return BadRequest();
            }
            return View(employee);
        }
    }
}
