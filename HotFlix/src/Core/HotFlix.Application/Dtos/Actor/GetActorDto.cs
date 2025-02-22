using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class GetActorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int MovieCount { get; set; }
     
    }
}
