
using HotFlix.Application.ViewModels;
using HotFlix.Domain.Enums;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotFlix.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _usermeneger;
        private readonly SignInManager<AppUser> _signinuser;
        private readonly AppDbContext _context;

        public AccountController(UserManager<AppUser> usermeneger, SignInManager<AppUser> signinuser,AppDbContext context)
        {
            _usermeneger = usermeneger;
            _signinuser = signinuser;
            _context = context;
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
            await _signinuser.SignInAsync(user, false);
            return RedirectToAction(nameof(HomeController.Index), "Home", new { Area = " " });
        }

        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM uservm, string? returnUrl)
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

        public async Task<IActionResult> Logout()
        {
            await _signinuser.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home", new { Area = " " });
        }

        [Route("google-login")]
        public IActionResult LoginWithGoogle()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
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
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            if(email == null)
            {
                return RedirectToAction("Login","Account");
            }
            var user=await _usermeneger.FindByEmailAsync(email);
            if(user == null)
            {
               user = new AppUser()
                {
                    Name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value,
                    Surname = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value,
                    Email =email,
                    UserName= claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value
               };
            }

            var createresult=await _usermeneger.CreateAsync(user);
            if (!createresult.Succeeded)
            {
                foreach (var error in createresult.Errors)
                {
                  return BadRequest(error.Description);
                }
            }
            await _signinuser.SignInAsync(user, true);
            return RedirectToAction(nameof(HomeController.Index), "Home", new { Area = " " });

        }

    }


}
