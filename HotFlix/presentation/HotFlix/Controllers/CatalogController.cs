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

            IQueryable<Movie> query =  _context.Movies;

            if(categoryId is not null || categoryId > 0)
            {
                query=query.Where(q=>q.CategoryId == categoryId);
            }

            if(tagId is not null || tagId > 0)
            {
                query = query.Include(q => q.MovieTags.Where(mt => mt.TagId == tagId));
            }

            switch (ratingkey)
            {
                case (int)RatingSort.From3:
                    query = query.OrderBy(q => q.Rating > 3);
                    break;
                case (int)RatingSort.From5:
                    query=query.OrderBy(q => q.Rating > 5);
                    break;
                case (int)RatingSort.From7:
                    query=query.OrderBy(query => query.Rating > 7);
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

            if (page < 1) return BadRequest();

            int count = _context.Movies.Count();
            double total = Math.Ceiling((double)count / 6);

            if(page>total) return BadRequest();

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
