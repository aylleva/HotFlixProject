using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.ViewModels;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;

namespace HotFlix.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMovieService _service;

        public HomeController(AppDbContext context,IMovieService service)
        {
            _context = context;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                AllMovies = await _context.Movies.Include(m => m.MovieTags).ThenInclude(mt => mt.Tag).Where(m=>m.Status==false).ToListAsync(),
                NewMovies = await _context.Movies.Include(m => m.MovieTags).ThenInclude(mt => mt.Tag).Where(m => m.Status == false)
                .Take(8)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync(),
                ExpectedMovies = await _context.Movies.Include(m => m.MovieTags).ThenInclude(mt => mt.Tag).Where(m => m.Status == false)
                .Take(8)
                .OrderByDescending(m => m.Premiere)
                .ToListAsync(),
                PremiumPlans=await _context.PremiumPlans.ToListAsync()
            };
            return View(homeVM);
        }
     
        public IActionResult Error(string errormessage)
        {
            return View(model: errormessage);
        }

        public IActionResult Language()
        {
            var defaultCultures = new[]
            {
                new CultureInfo("tr-TR"),
                new CultureInfo("en-US"),
            };

            CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var cultureItems = cinfo.Where(x => defaultCultures.Contains(x))
                .Select(c => new SelectListItem { Value = c.Name, Text = c.DisplayName })
                .ToList();
            ViewData["Cultures"] = cultureItems;

            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}