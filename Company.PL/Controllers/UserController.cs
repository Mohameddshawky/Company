using Company.BLL.AttachmentService;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.DAL.Models.Identitymodule;
using Company.PL.DTos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            var userquery= userManager.Users.AsQueryable();
            if (!String.IsNullOrEmpty(SearchValue))
            {
                userquery= userquery.Where(e=>e.Email.ToLower().Contains(SearchValue.ToLower()));
               
            }
            var users = userquery.Select(e => new UserDto
            {
                Email = e.Email,
                Id = e.Id,
                FirstName=e.Firstname, 
                LastName=e.Lastname

            }).ToList();
            foreach (var user in users)
            {
                user.Roles = await userManager.GetRolesAsync(await userManager.FindByIdAsync(user.Id));
            }

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string?id,string action="Details")
        {
            if (id == null) return BadRequest();

            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var userdto = new UserDto()
            {
                Email = user.Email,
                Id = user.Id,
                FirstName = user.Firstname,
                LastName = user.Lastname,
                Roles = await userManager.GetRolesAsync(user)
            };
            return View(action,userdto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string? id) {

            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserDto model,string id)
        {
            if (!ModelState.IsValid) return View(model);

            if (id!= model.Id) return BadRequest();

            var user = await userManager.FindByIdAsync(id);
            if(user == null) return NotFound();
            user.Firstname = model.FirstName;
            user.Lastname = model.LastName;
            user.Email = model.Email;
             var res=await userManager.UpdateAsync(user);
            if (res.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
           return await Details(id, "Delete");

            
        }
        [HttpPost]
      
        public async Task<IActionResult> Delete( string id, UserDto model)
        {
            if (ModelState.IsValid)
            {
                if (id == model.Id)
                {
                    var user=await userManager.FindByIdAsync(id);
                    if (user == null) return NotFound();
                     var res=await userManager.DeleteAsync(user);
                    if (res.Succeeded) return RedirectToAction(nameof(Index));
                }
                else
                    return BadRequest();
            }
            return View(model);
        }

    }
}
