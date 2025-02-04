using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Domain.Models
{
   public class Season:BaseEntity
    {
        public int SeasonNumber {  get; set; }
        public int MovieId {  get; set; }
        public Movie Movie { get; set; }
        public ICollection<Video> MovieVideos { get; set; }
    }
}
