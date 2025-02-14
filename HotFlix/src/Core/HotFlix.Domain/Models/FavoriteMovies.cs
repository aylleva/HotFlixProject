

namespace HotFlix.Domain.Models
{
    public class FavoriteMovies:BaseEntity
    {
        public string UserId {  get; set; }
        public AppUser User { get; set; }
        public int MovieId {  get; set; }
        public Movie Movie { get; set; }
    }
}
