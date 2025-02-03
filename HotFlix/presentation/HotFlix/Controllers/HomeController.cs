using HotFlix.Application.ViewModels;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotFlix.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                AllMovies = await _context.Movies.Include(m => m.MovieTags).ThenInclude(mt => mt.Tag).ToListAsync(),
                NewMovies= await _context.Movies.Include(m => m.MovieTags).ThenInclude(mt => mt.Tag)
                .Take(8)
                .OrderByDescending(m=>m.CreatedAt)
                .ToListAsync(),
                ExpectedMovies= await _context.Movies.Include(m => m.MovieTags).ThenInclude(mt => mt.Tag)
                .Take(8)
                .OrderByDescending(m=>m.Premiere)
                .ToListAsync()
            };
            return View(homeVM);
        }
    }
}
