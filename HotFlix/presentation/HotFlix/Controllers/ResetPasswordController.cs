using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.ViewModels.Password;
using HotFlix.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace HotFlix.Controllers
{
    public class ResetPasswordController : Controller
    {
        private readonly UserManager<AppUser> _usermeneger;
        private readonly IEmailService _emailservice;

        public ResetPasswordController(UserManager<AppUser> usermeneger,IEmailService emailservice)
        {
            _usermeneger = usermeneger;
            _emailservice = emailservice;
        }
        public IActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Forgot(ResetPasswordVM passwordvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _usermeneger.Users.FirstOrDefaultAsync(u => u.Email == passwordvm.Email);
            if(user is null)
            {
                ModelState.AddModelError(string.Empty, "User Is Not Found!Try Again!");
                return View();
            }

            Random random= new Random();
            string randomcode=random.Next(1000,1000).ToString();

          user.VerificationCode = randomcode;
          user.CodeExpiryTime = DateTime.UtcNow.AddMinutes(5);

          var result=await _usermeneger.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, "Something Went Wrong!");
                }
                return View();
            }

            _emailservice.SendEmail(user, user.VerificationCode);

            HttpContext.Session.SetString("Email", user.Email);
            return RedirectToAction(nameof(VerifyCode));
        }

        public IActionResult VerifyCode()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCode(VerifyCodeVM vm)
        {
            var userEmail = HttpContext.Session.GetString("Email");
            if (userEmail is null) return RedirectToAction(nameof(Forgot));

            if (!ModelState.IsValid) return View();

            var user = await _usermeneger.Users.FirstOrDefaultAsync(x => x.Email == userEmail);

            if (user == null) return View();

            if (user.VerificationCode != vm.Code || user.CodeExpiryTime < DateTime.UtcNow)
            {
                return View();
            }
            HttpContext.Session.SetString("Email", user.Email);
            return RedirectToAction(nameof(ChangePassword));
        }

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(string password)
        {
            var userEmail = HttpContext.Session.GetString("Email");
            if ((userEmail is null)) return RedirectToAction(nameof(Forgot));

            var user = await _usermeneger.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
            var token = await _usermeneger.GeneratePasswordResetTokenAsync(user);
            await _usermeneger.ResetPasswordAsync(user, token, password);
            HttpContext.Session.Remove("Email");

            TempData["SuccesMessage"] = $"<div class=\"text-center p-2 text-white bg-primary\">Your Password has been changed succesfully!!</div>";
            return View();
        }
    }
}
