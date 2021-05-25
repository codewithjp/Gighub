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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
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
                var user = await _userManager.FindByNameAsync(model.Email);

                return RedirectToAction("Index", "Gigs");
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

            var user = new IdentityUser {
            
            Email=model.Email,
            UserName=model.Email,
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
                return RedirectToAction("Index", "Gigs");
            }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            

            return View(model);
        }



    }
}
