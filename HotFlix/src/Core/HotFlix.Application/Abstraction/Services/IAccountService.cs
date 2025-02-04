using HotFlix.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Abstraction.Services
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterVM uservm);

        Task LoginAsync(LoginVM uservm);
      
    }
}
