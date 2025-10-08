using Company.DAL.Models.Identitymodule;
using Company.PL.DTos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Buffers;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    [Authorize("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public RoleController(
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager
            )
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult Index(string SearchValue)
        {
            var RoleQuery = roleManager.Roles.AsQueryable();
            if (!String.IsNullOrEmpty(SearchValue))
            {
                RoleQuery = RoleQuery.Where(e => e.Name.ToLower().Contains(SearchValue.ToLower()));

            }
            var Roles = RoleQuery.Select(e => new RoleDto
            {
                Id = e.Id,
                Name = e.Name,  

            }).ToList();
           
            return View(Roles);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string? id, string action = "Details")
        {
            if (id == null) return BadRequest();

            var Role = await roleManager.FindByIdAsync(id);
            if (Role == null) return NotFound();
            var userdto = new RoleDto()
            {
               
                Id = Role.Id,
                Name=Role.Name 
            };
            return View(action, userdto);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {


            if (id == null) return BadRequest();

            var Role = await roleManager.FindByIdAsync(id);
            if (Role == null) return NotFound();
            var Users = await userManager.Users.ToListAsync();
            var userdto = new RoleDto()
            {

                Id = Role.Id,
                Name = Role.Name,
                users =  Users.Select( u => new UserRoleDto
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    IsSelected = userManager.IsInRoleAsync(u, Role.Name).Result
                }).ToList()
            };
            return View(userdto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleDto model, string id)
        {
            if (!ModelState.IsValid) return View(model);

            if (id != model.Id) return BadRequest();
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
             role.Name = model.Name;    
            var res = await roleManager.UpdateAsync(role);
            foreach(var item in model.users)
            {
                var user = await userManager.FindByIdAsync(item.UserId);
                if(user is not null)
                {
                    if(item.IsSelected &&!( await userManager.IsInRoleAsync(user,role.Name)))
                    {
                        await userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if(!item.IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                    {
                        await userManager.RemoveFromRoleAsync(user, role.Name); 
                    }

                }

            }
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

        public async Task<IActionResult> Delete(string id, RoleDto model)
        {
            if (ModelState.IsValid)
            {
                if (id == model.Id)
                {
                    var role = await roleManager.FindByIdAsync(id);
                    if (role == null) return NotFound();
                    var res = await roleManager.DeleteAsync(role);
                    if (res.Succeeded) return RedirectToAction(nameof(Index));
                }
                else
                    return BadRequest();
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Create()=>View();

        [HttpPost]
        public async Task<IActionResult> Create(RoleDto model)
        {
            if (!ModelState.IsValid)return BadRequest();

            var Role = new IdentityRole()
            {
                Id = model.Id,
                Name = model.Name,
            };
            var res =await roleManager.CreateAsync(Role);
            if (res.Succeeded) return RedirectToAction(nameof(Index));
            return View(model);

        }


    }
}
