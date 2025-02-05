using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.ViewModels
{
   public class LoginVM
    {
        [MaxLength(256)]
        public string UsernameOrEmail {  get; set; }
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } 

        public string ReturnUrl {  get; set; }

        public IEnumerable<AuthenticationScheme> ExternalLogins { get; set; }  
        
    }
}
