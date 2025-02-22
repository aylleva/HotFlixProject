using Azure;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace HotFlix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int? page=1,int take=10)
        {
            if (page < 1) throw new Exception("Page was not found");

            int count = await _service.CountAsync();
            double total = Math.Ceiling((double)count / 10);

            if (page > total) throw new Exception("Page was not found");

            PaginatedItemDto<ContactDto> contactdto = new()
            {
                CurrectPage = page.Value,
                TotalPage = total,
                Items = _service.GetAllAsync(page.Value, take),
            };
            return View(contactdto);
        }

        public async Task<IActionResult> Detail(int? Id)
        {
            if(Id is null || Id<1) return BadRequest();
            return View(await _service.Detail(Id));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
