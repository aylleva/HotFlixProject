using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotFlix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int page=1,int take=10)
        {
            if (page < 1) throw new Exception("Page was not found");

            int count = await _service.CountAsync();
            double total = Math.Ceiling((double)count / 10);

            if (page > total) throw new Exception("Page was not found");

            PaginatedItemDto<UsersDto> users = new()
            {
                CurrectPage = page,
                TotalPage = total,
                Items = _service.GetAllAsync(page, take)
            };
            return View(users);
        }

        public async Task<IActionResult> Delete(string Id)
        {
            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeStatus(string Id)
        {
            await _service.ChangeStatus(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
