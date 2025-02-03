using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Domain.Models
{
    public class Director:BaseEntity
    {
        public string FullName {  get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
