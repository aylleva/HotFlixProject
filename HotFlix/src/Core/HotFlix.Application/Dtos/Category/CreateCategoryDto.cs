using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class CreateCategoryDto
    {
        [MaxLength(200, ErrorMessage = "Category Name Length should not exceed than 200")]
        public string Name {  get; set; }
    }
}
