using Company.DAL.Models.Identitymodule;
using Company.PL.DTos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
            )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDto model)
        {

            if (ModelState.IsValid) {
                var user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Firstname = model.FirstName,
                    Lastname = model.LastName,

                };

               var res=UserManager.CreateAsync(user, model.Password).Result;
                if (res.Succeeded)
                {
                    return RedirectToAction("login");
                }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(model);
                }
                
            }
            else
                return View(model);  
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDTo model)
        {
            if (ModelState.IsValid) {
            
                var user=await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var f=await UserManager.CheckPasswordAsync(user, model.Password);
                    if (f) {
                       var res= await SignInManager.PasswordSignInAsync(user, model.Password, model.RemberMe, false);
                        if (res.Succeeded)                 
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid Login");
                return View(model);

            }
            else
                return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
           await  SignInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

    }

}
