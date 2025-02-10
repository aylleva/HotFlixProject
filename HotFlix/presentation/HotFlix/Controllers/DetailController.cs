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
    public class DetailController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _usermeneger;

        public DetailController(AppDbContext context,UserManager<AppUser> usermeneger) 
        {
            _context = context;
            _usermeneger = usermeneger;
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

            if (movie is null) return NotFound();

            DetailVM detailVM = new DetailVM
            {
                Comments = comments,
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

      
       //public async Task<IActionResult> AddComment(CreateCommentVM commentvm,int? Id)
       // {
       //     if (!ModelState.IsValid)
       //     {
       //         return View();
       //     }
       //     var user = await _usermeneger.Users
       //         .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

          
       // }
    }
}
