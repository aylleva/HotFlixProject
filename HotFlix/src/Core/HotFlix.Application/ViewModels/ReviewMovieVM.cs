using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.ViewModels
{
    public class ReviewMovieVM
    {
        public int ID { get; set; } 
        public string Name {  get; set; }
        public string CategoryName {  get; set; }
        public double Rating {  get; set; }
        public DateTime Watched { get; set; }
    }
}
