using HotFlix.Application.ViewModels;
using HotFlix.Domain.Enums;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace HotFlix.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AppDbContext _context;

        public CatalogController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? tagId, int? categoryId, int ratingkey=1,int timekey=1,int page=1)
        {

            IQueryable<Movie> query =  _context.Movies.Where(m => m.Status == false);


            if(categoryId is not null || categoryId > 0)
            {
                query=query.Where(q=>q.CategoryId == categoryId);
            }

            if(tagId is not null || tagId > 0)
            {
                query =query.Where(m=>m.MovieTags.Any(mt=>mt.TagId == tagId));
            }

            switch (ratingkey)
            {
                case (int)RatingSort._3:
                    query = query.Where(q => q.Rating > 3);
                    break;
                case (int)RatingSort._5:
                    query=query.Where(q => q.Rating > 5);
                    break;
                case (int)RatingSort._7:
                    query=query.Where(query => query.Rating > 7);
                    break;
            }

            switch (timekey)
            {
                case (int)TimeSort.Newest:
                    query = query.OrderByDescending(q => q.Premiere);
                    break;
                 case (int)TimeSort.Oldest:
                    query=query.OrderBy(query=>query.Premiere);
                    break;
            }

            if (page < 1) throw new Exception("Sorry! Page Was Not Found");

            int count = _context.Movies.Count();
            double total = Math.Ceiling((double)count / 10);

            query = query.Skip((page - 1) * 10).Take(10);
            if (page>total) throw new Exception("Sorry! Page Was Not Found");

            CatalogVM catalogvm = new CatalogVM
            {
               Movies=await query.Select(m=> new GetMovieVM
               {
                   Id = m.Id,
                   Name = m.Name,
                   Image = m.Image,
                   Premiere=m.Premiere,
                   Rating=m.Rating
               }).ToListAsync(),
                Categories = await _context.Categories.Select(c => new GetCategoryVM
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync(),
                Tags = await _context.Tags.Select(t => new GetTagVm
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToListAsync(),
                PremierMovies=await _context.Movies.Include(m=>m.MovieTags)
                .ThenInclude(mt=>mt.Tag).Where(m => m.Status == false).OrderBy(m=>m.Premiere).ToListAsync(),

                CurrectPage = page,
                TotalPage = total,
                RatingKey = ratingkey,
                TimeKey = timekey,
                CategoryId = categoryId,
                TagId = tagId

            };

            return View(catalogvm);
        }
    }
}
