using HotFlix.Application.Abstraction.Services;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HotFlix.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;
  

        public SearchController(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task<IActionResult> SearchMovie(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new Exception("Movie Was Not Found");
            }

            ICollection<Movie> filteredMovies =await _context.Movies.Include(m=>m.MovieTags).ThenInclude(mt=>mt.Tag)
                .Where(m => m.Name.ToLower().Contains(query.ToLower()) && m.Status==false)
                .ToListAsync();

           if(filteredMovies.Count==0)
            {
                throw new Exception("Movie Was Not Found");
            }

            return View(filteredMovies);
        }
    }
}
