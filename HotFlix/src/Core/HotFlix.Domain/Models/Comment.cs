using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Domain.Models
{
    public class Comment:BaseEntity
    {
        public string Description {  get; set; }
        public string UserId {  get; set; }
        public AppUser User { get; set; }
        public int MovieId {  get; set; }
        public Movie Movie { get; set; }
    }
}
