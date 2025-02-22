using HotFlix.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class CreateActorDto
    {
        [MaxLength(200, ErrorMessage = "Full Name Length should not exceed than 200")]
        public string FullName { get; set; }
        public IFormFile Image { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Age can not be 0!")]
        [Required(ErrorMessage = "Age can not be empty!")]
        public int Age { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Movie Count can not be 0!")]
        [Required(ErrorMessage = "Movie Count can not be empty!")]
        public int MovieCount { get; set; }
       
    }
}
