using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class CreateCountryDto
    {
        [MaxLength(200, ErrorMessage = "Country Name Length should not exceed than 200")]
        public string Name { get; set; }    
    }
}
