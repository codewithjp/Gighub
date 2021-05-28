using Gighub.Data;
using Gighub.Utility;
using Gighub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Logoff()
        {
            await _signInManager.SignOutAsync();
            return  RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            return View(model);
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.IsRememberMe, false);
            if(result.Succeeded)
            {
               // var user = await _userManager.FindByNameAsync(model.Email);

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid Credentials");
            return View(model);
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new AppUser
            {
            
            Email=model.Email,
            UserName=model.Email,
            Name=model.Name
            };
            var result = await _userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.RoleName);
                if (!User.IsInRole(Helper.Admin))
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, null);
                }
                else
                {
                    TempData["newUserSignup"] = user.Email;
                }
                return RedirectToAction("Index", "Home");
            }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            

            return View(model);
        }



    }
}
