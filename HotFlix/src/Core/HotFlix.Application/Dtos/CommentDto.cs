

namespace HotFlix.Application.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LikeCount {  get; set; }
        public int DislikeCount {  get; set; }
        public string MovieName {  get; set; }

    }
}
