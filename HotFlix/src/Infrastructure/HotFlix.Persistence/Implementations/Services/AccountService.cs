using Azure.Core;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.ViewModels;
using HotFlix.Domain.Enums;
using HotFlix.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace HotFlix.Persistence.Implementations.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _usermeneger;
        private readonly SignInManager<AppUser> _signinuser;
        private readonly ModelStateDictionary _modelstate;

        public AccountService(UserManager<AppUser> usermeneger,SignInManager<AppUser> signinuser,ModelStateDictionary modelstate)
        {
           _usermeneger = usermeneger;
            _signinuser = signinuser;
            _modelstate = modelstate;
        }
        public async Task RegisterAsync(RegisterVM uservm)
        {
            AppUser user = new AppUser()
            {
                Name = uservm.Name,
                Surname = uservm.Surname,
                Email = uservm.Email,
                UserName = uservm.UserName,
            };

            var result=await _usermeneger.CreateAsync(user,uservm.Password);
            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    _modelstate.AddModelError(string.Empty, error.Description);
                }
            }
             await _usermeneger.AddToRoleAsync(user,UserRoles.Member.ToString());
             await  _signinuser.SignInAsync(user,true);  
        }

        public async Task LoginAsync(LoginVM uservm)
        {
          var user=await _usermeneger.Users.FirstOrDefaultAsync(u=>u.UserName==uservm.UsernameOrEmail || u.Email==uservm.UsernameOrEmail);
            if(user is null)
            {
                _modelstate.AddModelError(string.Empty, "UserName/Email or Password is inccorrect! Try Again!");
            }

            var result=await _signinuser.PasswordSignInAsync(user,uservm.Password,true,true);
            if (!result.Succeeded)
            {
                _modelstate.AddModelError(string.Empty, "UserName/Email or Password is inccorrect! Try Again!");
            }
            if(result.IsLockedOut)
            {
                _modelstate.AddModelError(string.Empty, "Your Account Has Been Blocked!");
            }
        }
    }
}
