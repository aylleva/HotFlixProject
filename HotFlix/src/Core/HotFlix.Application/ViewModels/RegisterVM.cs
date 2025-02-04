using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.ViewModels
{
   public class RegisterVM
    {
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
        [MinLength(3)]
        [MaxLength(40)]
        public string Surname {  get; set; }
        [MinLength(3)]
        [MaxLength(256)]
        public string UserName {  get; set; }
        [MinLength(6)]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))] 
        public string ConfirmPassword {  get; set; }
    }
}
