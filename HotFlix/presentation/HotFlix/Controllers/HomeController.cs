using HotFlix.Application.ViewModels;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HotFlix.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                AllMovies = await _context.Movies.Include(m => m.MovieTags).ThenInclude(mt => mt.Tag).ToListAsync(),
                NewMovies = await _context.Movies.Include(m => m.MovieTags).ThenInclude(mt => mt.Tag)
                .Take(8)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync(),
                ExpectedMovies = await _context.Movies.Include(m => m.MovieTags).ThenInclude(mt => mt.Tag)
                .Take(8)
                .OrderByDescending(m => m.Premiere)
                .ToListAsync(),
                PremiumPlans=await _context.PremiumPlans.ToListAsync()
            };
            return View(homeVM);
        }

        public IActionResult ChangeLanguage(string lang)
        {
            if(!string.IsNullOrEmpty(lang))
            {
                Thread.CurrentThread.CurrentCulture=CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentUICulture=new CultureInfo(lang);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                lang = "en";
            }
            Response.Cookies.Append("Language", "en");
            return Redirect(Request.GetTypedHeaders().Referer.ToString());
        }
        public IActionResult Error(string errormessage)
        {
            return View(model: errormessage);
        }
    }
}