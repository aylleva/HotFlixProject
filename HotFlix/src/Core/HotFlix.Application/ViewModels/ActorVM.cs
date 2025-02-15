using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.ViewModels
{
    public class ActorVM
    {
        public Actor Actor { get; set; }
        public int CurrectPage {  get; set; }
        public double TotalPage {  get; set; }
        public ICollection<Movie> Movies { get; set; }  
    }
}
