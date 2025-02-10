using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.ViewModels.Password
{
   public class VerifyCodeVM
    {
        [Required(ErrorMessage ="Code Is Required")]
        public string Code { get; set; }
    }
}
