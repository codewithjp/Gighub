using Gighub.Data;
using Gighub.Utility;
using Gighub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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



        
        [HttpPost]
       
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider,properties);
        }


       
       // [Route("signin-google")]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback
            (string retunUrl=null,string remoteError=null)
        {
            retunUrl ??= Url.Content("~/");
            var loginVM = new LoginViewModel
            {
                ReturnUrl=retunUrl,
                ExternalLogins=(await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError!=null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider :{remoteError}");
                return View("Login", loginVM);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, $"Error loading external login information ");
                return View("Login", loginVM);
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync
                (info.LoginProvider, info.ProviderKey, isPersistent: false,bypassTwoFactor:true);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(retunUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email!=null)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user==null)
                    {
                        user = new AppUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Name=info.Principal.FindFirstValue(ClaimTypes.Name),
                            
                        };
                        await _userManager.CreateAsync(user);
                        await _userManager.AddToRoleAsync(user,Helper.Artist);
                    }
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(retunUrl);
                }
                ViewBag.ErrorTitle = $"Email claim not received from {info.LoginProvider}";
                ViewBag.Error = "Please contact to info@gighub.com";
            }

            return View("Error");
        }



        public async Task<IActionResult> Login(string returnUrl)
        {
            var loginVM = new LoginViewModel
            {
               ReturnUrl=returnUrl,
               ExternalLogins= (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()

            };

            return View(loginVM);
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
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
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
