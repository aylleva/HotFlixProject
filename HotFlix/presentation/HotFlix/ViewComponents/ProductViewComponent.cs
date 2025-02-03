using HotFlix.Domain.Enums;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HotFlix.ViewComponents
{
    public class ProductViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public ProductViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(SortType sort)
        {
            List<Movie> movies=null;
            switch(sort)
            {
                case SortType.Action:
                    movies=await _context.Movies.Where(m=>m.Category.Name=="Action")
                        .Include(m=>m.MovieTags).ThenInclude(mt=>mt.Tag)
                        .Take(10)
                        .ToListAsync(); 
                    break;
                case SortType.Mystery:
                    movies = await _context.Movies.Where(m => m.Category.Name == "Mystery")
                        .Include(m => m.MovieTags).ThenInclude(mt => mt.Tag)
                         .Take(10)
                        .ToListAsync();
                    break;
                case SortType.Comedy:
                    movies = await _context.Movies.Where(m => m.Category.Name == "Comedy")
                        .Include(m => m.MovieTags).ThenInclude(mt => mt.Tag)
                         .Take(10)
                        .ToListAsync();
                    break;
               
            }
            return View(movies);
        }
    }
}
