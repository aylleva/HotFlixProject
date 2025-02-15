using HotFlix.Application.ViewModels;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Security.Claims;

namespace HotFlix.Controllers
{
    public class DetailController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _usermeneger;

        public DetailController(AppDbContext context,UserManager<AppUser> usermeneger) 
        {
            _context = context;
            _usermeneger = usermeneger;
        }
        public async Task<IActionResult> Index(int? Id,int? sezonId,int? serieId)
        {
            if (Id is null || Id < 1) throw new Exception("Sorry! Movie Was Not Found");

            var user = await _usermeneger.Users.FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));


            Movie? movie = await _context.Movies
                .Include(m => m.MovieTags).ThenInclude(m => m.Tag)
                .Include(m => m.MovieActors).ThenInclude(m => m.Actor)
                .Include(m => m.Category)
                .Include(m => m.Director)
                .Include(m => m.Country)
                .Include(m=>m.Seasons).ThenInclude(s=>s.SeasonVideos)
                .FirstOrDefaultAsync(m => m.Id == Id);

            if (movie is null) throw new Exception("Sorry! Movie Was Not Found");

            ICollection<GetCommentsVM>? comments=await _context.Comments.Include(c=>c.User).Include(u=>u.Movie)
                .Where(c=>c.MovieId==Id)
                .Select(c=> new GetCommentsVM
                {
                    Id = c.Id,
                    Description = c.Description,
                    UserId=c.UserId,
                    LikeCount = c.LikeCount,
                    DislikeCount=c.DisLikeCount,
                    CreatedAt = c.CreatedAt,
                    UserName=c.User.UserName
                }).ToListAsync();

            DetailVM detailVM = new DetailVM
            {
                Comments = comments,
                Movie = movie,
                RelatedMovies = await _context.Movies
                .Include(m => m.MovieTags).ThenInclude(m => m.Tag)
            .Where(m => m.CategoryId == movie.CategoryId && m.Id != Id)
            .ToListAsync(),
                SerieId = serieId,
                SezonId = sezonId,
               
            };

           if(user is not null)
            {
                MovieWatches watches = new MovieWatches()
                {
                    MovieId = Id.Value,
                    UserId = user.Id,
                    WatchedAt = DateTime.UtcNow
                };
                await _context.MovieWatches.AddAsync(watches);
                user.WatchedFilms++;
                await _context.SaveChangesAsync();
            }

            return View(detailVM);
        }

        public async Task<IActionResult> Actor(int? Id,int page=1)
        {
            if(Id is null || Id<1) throw new Exception("Sorry! Actor Was Not Found");

            if(page<1) throw new Exception("Sorry! Page Was Not Found");

            int count = await _context.Movies.CountAsync();
            double total = Math.Truncate((double)count /5);

            if (page > total) throw new Exception("Sorry! Page Was Not Found");

            Actor? actor=await _context.Actors
                .Include(a=>a.MovieActors).ThenInclude(ma=>ma.Movie)
                .ThenInclude(m=>m.MovieTags).ThenInclude(m=>m.Tag)
                .FirstOrDefaultAsync(a=>a.Id==Id);

            if (actor is null) throw new Exception("Sorry! Actor Was Not Found");

            ActorVM actorvm = new ActorVM
            {
                Actor = actor,
                CurrectPage = page,
                TotalPage = total,
                Movies=await _context.Movies.Include(m=>m.MovieTags).ThenInclude(m=>m.Tag).Skip((page - 1) *5).Take(5).Where(m=>m.MovieActors.Any(m=>m.ActorId==Id)).ToListAsync()

            };
            return View(actorvm); 
        }


        //public async Task<IActionResult> AddComment(CreateCommentVM commentvm, int? Id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    var user = await _usermeneger.Users
        //        .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));


        //}
    }
}
