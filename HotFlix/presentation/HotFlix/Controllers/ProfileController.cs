using HotFlix.Application.ViewModels;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HotFlix.Controllers
{

    
    public class ProfileController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _usermeneger;

        public ProfileController(AppDbContext context,UserManager<AppUser> usermeneger)
        {
            _context = context;
            _usermeneger = usermeneger;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _usermeneger.Users.Include(u=>u.Reviews).Include(u => u.Comments).Include(u=>u.PremiumPlan).FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            ProfileVM profilevm = new ProfileVM
            {
                FavoriteMovies = await _context.FavoriteMovies.Where(f => f.UserId == user.Id)
                    .Select(f => new FavoriteMoviesVM
                    {
                        MovieId = f.MovieId,
                        Name = f.Movie.Name,
                        Rating = f.Movie.Rating,
                        CategoryName = f.Movie.Category.Name,
                        Image = f.Movie.Image
                    }).ToListAsync(),
                ReviewedMovie=await _context.MovieWatches.Include(m=>m.Movie).ThenInclude(m=>m.Category).Where(m=>m.UserId==user.Id)
                 .Select(m=>new ReviewMovieVM
                 {
                     ID=m.MovieId,
                     Name= m.Movie.Name,
                     Rating= m.Movie.Rating,
                     CategoryName= m.Movie.Category.Name,
                     Watched=m.WatchedAt

                   }).OrderByDescending(m=>m.Watched).Take(5)
                 .ToListAsync(),
                CommentCount = user.Comments?.Count(),
                PremiumPLan = user.PremiumPlan?.PlanName,
                MovieViews = user.WatchedFilms,
                ReviewCount = user.Reviews?.Count()
            };

            if(profilevm is null)
            {
                return View();
            }
            return View(profilevm);
        }

        public async Task<IActionResult> AddMovie(int? Id)
        {
            if (Id is null || Id < 1) throw new Exception("Sorry! Movie Was Not Found");
            var result=await _context.Movies.AnyAsync(m=>m.Id == Id);
            if(!result) throw new Exception("Sorry! Movie Was Not Found");

            var user = await _usermeneger.Users.Include(u=>u.FavoriteMovies).FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            var movie = await _context.FavoriteMovies.FirstOrDefaultAsync(w => w.MovieId == Id);

            if (movie is null)
            {
                user.FavoriteMovies.Add(new FavoriteMovies { MovieId = Id.Value });
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(HomeController.Index),"Home");
        }

   
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id is null || Id < 1) throw new Exception("Sorry! Movie Was Not Found");
            var result = await _context.Movies.AnyAsync(m => m.Id == Id);
            if (!result) throw new Exception("Sorry! Movie Was Not Found");

            var user = await _usermeneger.Users.Include(u => u.FavoriteMovies).FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            var movie = await _context.FavoriteMovies.FirstOrDefaultAsync(w => w.MovieId == Id);

            if (movie is not null)
            {
                user.FavoriteMovies.Remove(movie);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ProfileController.Index), "Profile");
        }

    }
}
