using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.ViewModels
{
    public class HomeVM
    {
        public ICollection<Movie> ExpectedMovies { get; set; }
        public ICollection<Movie> NewMovies { get; set; }
        public ICollection<Movie> AllMovies { get; set; }
    }
}
