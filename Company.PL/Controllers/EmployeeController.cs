using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.DTos;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeController(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {

            var employees = employeeRepository.GetAll().ToList();
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
            if (ModelState.IsValid)
            {//server side validation
                var employee = new Employee()
                {
                    Name = model.Name,
                    Age = model.Age,
                    Address = model.Address,    
                    Email = model.Email,    
                    Phone = model.Phone,    
                    IsActive = model.IsActive,  
                    IsDeleted = model.IsDeleted,    
                    CreateAt = model.CreateAt,  
                    HiringDate = model.HiringDate,  
                    Salary=model.Salary,          
                };
                var cnt = employeeRepository.Add(employee);
                if (cnt > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var employee = employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, Message = "Employee is not found" });

            return View(ViewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//prefer for any post action
        //prevent any one to request rather than client side
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id == employee.Id)
                {
                    var cnt = employeeRepository.Update(employee);
                    if (cnt > 0) return RedirectToAction(nameof(Index));
                }
                else
                    return BadRequest();
            }
            return View(employee);
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
                    var cnt = employeeRepository.Delete(employee);
                    if (cnt > 0) return RedirectToAction(nameof(Index));
                }
                else
                    return BadRequest();
            }
            return View(employee);
        }
    }
}
