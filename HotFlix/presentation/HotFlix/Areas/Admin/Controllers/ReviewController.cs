using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotFlix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
           _service = service;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 10)
        {
            if (page < 1) throw new Exception("Page was not found");

            int count = await _service.CountAsync();
            double total = Math.Ceiling((double)count / 2);

            if (page > total) throw new Exception("Page was not found");

            PaginatedItemDto<ReviewDto> reviewDto = new()
            {
                CurrectPage = page,
                TotalPage = total,
                Items = _service.GetAllAsync(page, take),
            };
            return View(reviewDto);
          
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
