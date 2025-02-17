

namespace HotFlix.Domain.Models
{
    public class Review:BaseEntity
    {
        public string Title {  get; set; }
        public int RatingId {  get; set; }
        public int MovieId {  get; set; }
        public Movie Movie { get; set; }
        public string UserId {  get; set; }
        public AppUser User { get; set; }
        public Rating? Ratings { get; set; }
    }
}
