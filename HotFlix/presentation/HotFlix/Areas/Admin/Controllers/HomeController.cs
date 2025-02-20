using HotFlix.Application.Dtos;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotFlix.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext  context)
        {
           _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeDto homeDto = new HomeDto()
            {
                SubscriptionCount = _context.PlanOrders.Count(),
                ReviewsCount = _context.Reviews.Count(),
                AddedItemsCount = _context.FavoriteMovies.Count(),
                WatchedMoviesCount = _context.MovieWatches.Count(),
                LatestMovies = await _context.Movies.Include(m => m.Category).OrderByDescending(m => m.CreatedAt).Take(5).ToListAsync(),
                BestMovies = await _context.Movies.Include(m => m.Category).OrderByDescending(m => m.Rating).Take(5).ToListAsync(),
                ExpectedMovies= await _context.Movies.Include(m => m.Category).OrderByDescending(m => m.Premiere).Take(5).ToListAsync(),
                Users = await _context.Users.ToListAsync()
            };


            return View(homeDto);
        }

        public IActionResult Error(string errormessage)
        {
            return View(model: errormessage);
        }
    }
}
