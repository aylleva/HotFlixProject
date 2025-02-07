using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.ViewModels
{
    public class GetMovieVM
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public double Rating {  get; set; }
        public int Premiere {  get; set; }
        public string Image {  get; set; }
    }
}
