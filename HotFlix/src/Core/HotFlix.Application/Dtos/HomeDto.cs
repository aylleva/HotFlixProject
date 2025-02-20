using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class HomeDto
    {
        public int? SubscriptionCount {  get; set; }
        public int? AddedItemsCount {  get; set; }
        public int? WatchedMoviesCount {  get; set; }
        public int? ReviewsCount {  get; set; }
        public ICollection<Movie>? LatestMovies { get; set; }
        public ICollection<Movie>? BestMovies { get; set; }
        public ICollection<Movie>? ExpectedMovies { get; set; }
        public ICollection<AppUser>? Users { get; set; }
    }
}
