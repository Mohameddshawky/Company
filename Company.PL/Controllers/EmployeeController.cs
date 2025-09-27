using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.DTos;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;

        public EmployeeController(
            IEmployeeRepository _employeeRepository,
           IDepartmentRepository _departmentRepository,
           IMapper mapper)
        {
            employeeRepository = _employeeRepository;
            departmentRepository = _departmentRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees;
            if (String.IsNullOrEmpty(EmployeeSearchName))
                 employees = employeeRepository.GetAll().ToList();
            else
                employees= employeeRepository.Search(EmployeeSearchName);
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
            {
                var employee = mapper.Map<Employee>(model);
                var cnt = employeeRepository.Add(employee);
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

            var employee = employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, Message = "Employee is not found" });

            return View(ViewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id is null) return BadRequest();

            var model = employeeRepository.Get(id.Value);

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
                
                var employee = mapper.Map<Employee>(model);
                employee.Id = id;
         


                var cnt = employeeRepository.Update(employee);
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
