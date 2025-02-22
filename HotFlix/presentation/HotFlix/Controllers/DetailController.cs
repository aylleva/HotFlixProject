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
            if (Id is null || Id < 1) throw new Exception("Sorry! Movie Was Not Found");

            var user = await _usermeneger.Users.FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));


            Movie? movie = await _context.Movies
                .Include(m => m.MovieTags).ThenInclude(m => m.Tag)
                .Include(m => m.MovieActors).ThenInclude(m => m.Actor)
                .Include(m => m.Category)
                .Include(m => m.Director)
                .Include(m => m.Country)
                .Include(m=>m.Seasons).ThenInclude(s=>s.SeasonVideos)
                .Where(m => m.Status == false)
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
                    UserName=c.User.UserName,
                    MovieId=c.MovieId,
                    UserImage=c.User.Image

                }).ToListAsync();

            ICollection<GetReviewsVm>? reviews = await _context.Reviews.Include(r => r.Ratings).Include(c => c.User).Include(u => u.Movie)
               .Where(c => c.MovieId == Id)
               .Select(c => new GetReviewsVm
               {
                   Id = c.Id,
                   Title = c.Title,
                   UserId = c.UserId,
                   CreatedAt = c.CreatedAt,
                   UserName = c.User.UserName,
                   MovieId = c.MovieId,
                   Rating = c.Ratings.RatingNumber,
                   Image = c.User.Image

               }).ToListAsync();



            DetailVM detailVM = new DetailVM
            {
                Reviews = reviews,
                Comments = comments,
                Movie = movie,
                RelatedMovies = await _context.Movies
                .Include(m => m.MovieTags).ThenInclude(m => m.Tag)
            .Where(m => m.CategoryId == movie.CategoryId && m.Id != Id)
            .ToListAsync(),
               Ratings=await _context.Ratings.ToListAsync()                          
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

        [Authorize]
        public async Task<IActionResult> AddComment(DetailVM commentvm, int? Id)
        {
            if (Id is null || Id < 1) throw new Exception("Sorry! Movie Was Not Found");

            if (!ModelState.IsValid)
            {
                return View(commentvm);
            }
            var user = await _usermeneger.Users
                .Include(u=>u.Comments).FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            var movie = await _context.Movies.Include(m=>m.Comments).FirstOrDefaultAsync(m => m.Id == Id);
            if (movie is null) throw new Exception("Movie Was Not Found");

            Comments comment = new Comments
            {
                Description = commentvm.NewComment.Description,
                CreatedAt = DateTime.Now,
                MovieId = Id.Value,
                UserId = user.Id,
                LikeCount = 0,
                DisLikeCount = 0,
                HasDisliked =false,
                HasLiked =false
            };
            movie.Comments.Add(comment);
            user.Comments.Add(comment);
            await _context.SaveChangesAsync();  

            return RedirectToAction("Index","Detail",new {id=Id});
        }

        [Authorize]
        public async Task<JsonResult> LikeComment(int? Id,int? movieId)
        {
            if (Id is null || Id < 1) throw new Exception("Sorry! Comment Was Not Found");

            var comment=await _context.Comments.FirstOrDefaultAsync(c=>c.Id== Id && c.MovieId==movieId);
            
            if(comment is not null)
            {
                comment.LikeCount++;
                comment.HasLiked = true;

                if (comment.HasDisliked==true)
                {
                    comment.DisLikeCount--;
                    comment.HasDisliked = false;
                }
            }
            await _context.SaveChangesAsync();

            return  Json(new { success = true, likeCount = comment.LikeCount, dislikeCount = comment.DisLikeCount });
        }
        [Authorize]
        public async Task<JsonResult> DislikeComment(int? Id, int? movieId)
        {
            if (Id is null || Id < 1) throw new Exception("Sorry! Comment Was Not Found");

            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == Id && c.MovieId == movieId);

            if (comment is not null)
            {
                comment.DisLikeCount++;
                comment.HasDisliked= true;

                if (comment.HasLiked == true)
                {
                    comment.LikeCount--;
                    comment.HasLiked= false;
                }
            }
            await _context.SaveChangesAsync();

            return Json(new { success = true, likeCount = comment.LikeCount, dislikeCount = comment.DisLikeCount });
        }
        [Authorize]
        public async Task<IActionResult> Delete(int? Id, int? movieId)
        {
            if (Id is null || Id < 1) throw new Exception("Sorry! Comment Was Not Found");

            var user = await _usermeneger.Users
           .Include(u => u.Comments).FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            var movie=await _context.Movies.Include(m=>m.Comments).FirstOrDefaultAsync(m=>m.Id==movieId);  

            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == Id && c.MovieId == movieId);

            if (comment is not null)
            {
               movie.Comments.Remove(comment);
               user.Comments.Remove(comment);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Detail", new { id = movieId });
        }

        [Authorize] 
        public async Task<IActionResult> AddReview(DetailVM reviewvm,int? Id)
        {

            if (Id is null || Id < 1) throw new Exception("Sorry! Movie Was Not Found");

            if (!ModelState.IsValid)
            {
                return View(reviewvm);
            }
            var user = await _usermeneger.Users
                .Include(u => u.Reviews).FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _context.Ratings.AnyAsync(r => r.Id == reviewvm.NewReview.RatingId);
            if (!result)
            {
                ModelState.AddModelError(nameof(CreateReviewVm.RatingId), "Rating Does not exists");
                return View(reviewvm);
            }

            var movie = await _context.Movies.Include(m => m.Reviews).FirstOrDefaultAsync(m => m.Id == Id);
            if (movie is null) throw new Exception("Movie Was Not Found");

            Review review = new Review()
            {
                Title = reviewvm.NewReview.Title,
                MovieId = Id.Value,
                UserId = user.Id,
                CreatedAt = DateTime.Now,
                RatingId = reviewvm.NewReview.RatingId
            };
            
            movie.Reviews.Add(review);
            user.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Detail", new { id = Id });
        }
    }
}
