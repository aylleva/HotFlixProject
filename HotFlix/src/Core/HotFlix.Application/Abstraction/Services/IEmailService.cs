using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Abstraction.Services
{
   public interface IEmailService
    {
        public void SendEmail(AppUser user,string randomcode);
    }
}
