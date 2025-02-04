using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Domain.Models
{
    public class Video:BaseEntity
    {
        public string VideoURL {  get; set; }
        public int SeasonId {  get; set; }
        public Season Season { get; set; }
    }
}
