
namespace HotFlix.Domain.Models
{
   public class Season:BaseEntity
    {
        public int SeasonNumber {  get; set; }
        public int MovieId {  get; set; }
        public Movie Movie { get; set; }
        public ICollection<SeasonVideos> SeasonVideos { get; set; }
    }
}
