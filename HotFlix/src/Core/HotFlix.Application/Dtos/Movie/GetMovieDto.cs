using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class GetMovieDto
    {
        public string Name { get; set; }
        public double Rating { get; set; }
        public string CategoryName {  get; set; }
        public bool Status {  get; set; }
        public int Views {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
