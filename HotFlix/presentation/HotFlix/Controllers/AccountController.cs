using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.ViewModels;
using HotFlix.Domain.Enums;
using HotFlix.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace HotFlix.Controllers
{
    public class AccountController : Controller
    {
     
        private readonly UserManager<AppUser> _usermeneger;
        private readonly SignInManager<AppUser> _signinuser;

        public AccountController(UserManager<AppUser> usermeneger, SignInManager<AppUser> signinuser)
        {
            _usermeneger = usermeneger;
            _signinuser = signinuser;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM uservm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            {
                Name = uservm.Name,
                Surname = uservm.Surname,
                Email = uservm.Email,
                UserName = uservm.UserName,
            };

            var result = await _usermeneger.CreateAsync(user, uservm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }

            await _usermeneger.AddToRoleAsync(user, UserRoles.Member.ToString());
            await _signinuser.SignInAsync(user,false);
            return RedirectToAction(nameof(HomeController.Index), "Home", new {Area=" "});
        }

        public  IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM uservm,string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _usermeneger.Users.FirstOrDefaultAsync(u => u.UserName == uservm.UsernameOrEmail || u.Email == uservm.UsernameOrEmail);
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "UserName/Email or Password is inccorrect! Try Again!");
                return View();
            }

            var result = await _signinuser.PasswordSignInAsync(user, uservm.Password, true, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "UserName/Email or Password is inccorrect! Try Again!");
                return View();
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Your Account Has Been Blocked!");
                return View();
            }

            if (returnUrl is null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home", new { Area = " " });
            }
            return Redirect(returnUrl);
        }
          public async Task LoginWithGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });

        }
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Value,
                claim.Type
            });
            return RedirectToAction(nameof(HomeController.Index), "Home", new { Area = " " });
        }


        public async Task<IActionResult> Logout()
        {
            await _signinuser.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home", new { Area = " " });
        }
        //public async Task<IActionResult> LoginWithGoogle(string provider, string returnUrl = "")
        //   {
        //       var redirectUrl = Url.Action("ExternalLogin", "Account", new { ReturnUrl = returnUrl });

        //       var properties = _signinuser.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        //       return new ChallengeResult(provider, properties);
        //   }

        //public async Task<IActionResult> ExternalLogin(string returnUrl = "", string remoteerror = "")
        //{
        //    LoginVM loginVM = new LoginVM()
        //    {
        //        Schemes = await _signinuser.GetExternalAuthenticationSchemesAsync()
        //    };

        //    if (!string.IsNullOrEmpty(remoteerror))
        //    {
        //        ModelState.AddModelError(string.Empty, $"{remoteerror}");
        //        return View("Login", loginVM);
        //    }

        //    var info = await _signinuser.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        ModelState.AddModelError(string.Empty, $"{remoteerror}");
        //        return View("Login", loginVM);
        //    }

        //    var result = await _signinuser.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
        //        false, true);
        //    if (!result.Succeeded)
        //    {
        //        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //        if (!string.IsNullOrEmpty(email))
        //        {
        //            var user = await _usermeneger.FindByEmailAsync(email);

        //            if (user is null)
        //            {
        //                var newuser = new AppUser()
        //                {
        //                    Name = email,
        //                    Surname = email,
        //                    Email = email,
        //                    UserName = email
        //                };
        //                await _usermeneger.CreateAsync(user);
        //                await _usermeneger.AddToRoleAsync(user, UserRoles.Member.ToString());
        //            }
        //            await _signinuser.SignInAsync(user, false);

        //            return RedirectToAction(nameof(HomeController.Index), "Home", new { Area = " " });
        //        }

        //    }
        //    ModelState.AddModelError(string.Empty, "Ups!Something is wrong!");
        //    return View("Login", loginVM);
    }
    }
