
namespace HotFlix.Application.ViewModels
{
    public class ProfileVM
    {
        public ICollection<FavoriteMoviesVM>? FavoriteMovies { get; set; }
        public int? CommentCount { get; set; }   
        public string? PremiumPLan {  get; set; }
        public int? MovieViews {  get; set; }
        public ICollection<ReviewMovieVM>? ReviewedMovie { get; set; }
    }
}
