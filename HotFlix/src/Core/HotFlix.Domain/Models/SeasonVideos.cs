using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Domain.Models
{
    public class SeasonVideos:BaseEntity
    {
        public int SeriesNumber {  get; set; }
        public int SeasonId {  get; set; }
        public string VideoUrl {  get; set; }
    }
}
