

namespace HotFlix.Application.ViewModels
{
    public class GetReviewsVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public int Rating {  get; set; }
        public string UserName { get; set; }
        public int MovieId { get; set; }
    }
}
