using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Domain.Models
{
    public class MovieTags
    {
        public int MovieId {  get; set; }
        public int TagId {  get; set; }
        public Movie Movie { get; set; }
        public Tag Tag { get; set; }
    }
}
