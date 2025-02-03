

namespace HotFlix.Domain.Models
{
   public class MovieVideos:BaseEntity
    {
        public string VideoURL {  get; set; }
        public int MovieId {  get; set; }
        public Movie Movie { get; set; }
    }
}
