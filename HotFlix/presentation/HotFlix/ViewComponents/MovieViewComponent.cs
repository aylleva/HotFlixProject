using HotFlix.Domain.Enums;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HotFlix.ViewComponents
{
    public class MovieViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public MovieViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(SortType sort)
        {
            List<Movie> movies=null;
            switch(sort)
            {
                case SortType.Movie:
                    movies=await _context.Movies.Where(m=>m.Category.Name=="Movies")
                        .Where(m => m.Status == false)
                        .Include(m=>m.MovieTags).ThenInclude(mt=>mt.Tag)
                        .Take(10)
                        .ToListAsync(); 
                    break;
                case SortType.TVSeries:
                    movies = await _context.Movies.Where(m => m.Category.Name == "Tv Series")
                        .Where(m => m.Status == false)
                        .Include(m => m.MovieTags).ThenInclude(mt => mt.Tag)
                         .Take(10)
                        .ToListAsync();
                    break;
              
               
            }
            return View(movies);
        }
    }
}
