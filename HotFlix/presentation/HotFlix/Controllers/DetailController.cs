using HotFlix.Application.ViewModels;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotFlix.Controllers
{
    public class DetailController : Controller
    {
        private readonly AppDbContext _context;

        public DetailController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? Id)
        {
            if (Id is null || Id < 1) return BadRequest();

            Movie? movie = await _context.Movies
                .Include(m => m.MovieTags).ThenInclude(m => m.Tag)
                .Include(m => m.MovieActors).ThenInclude(m => m.Actor)
                .Include(m => m.Category)
                .Include(m => m.Director)
                .Include(m => m.Country)
                .Include(m=>m.Seasons).ThenInclude(s=>s.SeasonVideos)
                .FirstOrDefaultAsync(m => m.Id == Id);

            if (movie is null) return NotFound();

            DetailVM detailVM = new DetailVM
            {
                Movie = movie,
                RelatedMovies = await _context.Movies
                .Include(m => m.MovieTags).ThenInclude(m => m.Tag)
            .Where(m => m.CategoryId == movie.CategoryId && m.Id!=Id)
            .ToListAsync()
            };

            return View(detailVM);
        }

        public async Task<IActionResult> Actor(int? Id)
        {
            Actor? actor=await _context.Actors
                .Include(a=>a.MovieActors).ThenInclude(ma=>ma.Movie)
                .ThenInclude(m=>m.MovieTags).ThenInclude(m=>m.Tag)
                .FirstOrDefaultAsync(a=>a.Id==Id);
            return View(actor); 
        }
    }
}
