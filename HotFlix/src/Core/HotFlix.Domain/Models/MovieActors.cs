using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Domain.Models
{
    public class MovieActors
    {
        public int MovieId {  get; set; }
        public int ActorId {  get; set; }
        public Actor Actor { get; set; }
        public Movie Movie { get; set; }
    }
}
