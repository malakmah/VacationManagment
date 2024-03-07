using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationManagement.Data;
using VacationManagement.Models;

namespace VacationManagement.Controllers
{
    public class AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : Controller 
    {
     

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid) 
            {
                var result =await signInManager.PasswordSignInAsync(model.UserName!,model.Password!,model.RememberMe,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterLogin model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                { 
                    Name=model.Name,
                    UserName=model.Name,
                    Email=model.Email,
                    Address=model.Address,
                    
                };
                var result=await userManager.CreateAsync(user,model.Password!);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index));
        }
    }
}
