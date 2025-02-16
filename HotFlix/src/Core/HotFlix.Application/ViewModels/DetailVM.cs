using HotFlix.Domain.Models;


namespace HotFlix.Application.ViewModels
{
   public  class DetailVM
    {
        public ICollection<Movie>? RelatedMovies { get; set; }
        public Movie? Movie { get; set; }
        public ICollection<GetCommentsVM>? Comments { get; set; }
        public CreateCommentVM? NewComment { get; set; }
        
    }
}
