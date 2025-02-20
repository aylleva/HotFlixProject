

namespace HotFlix.Domain.Models
{
    public class Comments:BaseEntity
    {
        public string Description {  get; set; }
        public string UserId {  get; set; }
        public AppUser User { get; set; }
        public int MovieId {  get; set; }
        public Movie Movie { get; set; }
        public int? LikeCount {  get; set; }
        public int? DisLikeCount { get; set; }
        public bool? HasLiked { get; set; }
        public bool? HasDisliked { get; set; }
    }
}
