

using HotFlix.Domain.Models;

namespace HotFlix.Application.ViewModels
{
    public class CatalogVM
    {
        public ICollection<GetMovieVM> Movies { get; set; }
        public ICollection<GetCategoryVM> Categories { get; set; }
        public ICollection<GetTagVm> Tags { get; set; }
        public int RatingKey {  get; set; }
        public int TimeKey {  get; set; }
        public int CurrectPage {  get; set; }
        public double TotalPage {  get; set; }
        public int? CategoryId {  get; set; }
        public int? TagId {  get; set; }
        public ICollection<Movie> PremierMovies { get; set; }
    }
}
