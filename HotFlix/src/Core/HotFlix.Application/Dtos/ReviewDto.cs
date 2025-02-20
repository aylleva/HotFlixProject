
namespace HotFlix.Application.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Rating {  get; set; }
        public string MovieName { get; set; }
    }
}
