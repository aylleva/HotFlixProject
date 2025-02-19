using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace HotFlix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagDto tagDto)
        {
            if (!ModelState.IsValid)
            {
                return View(tagDto);
            }

            var result = await _service.AnyAsync(c => c.Name == tagDto.Name);
            if (result)
            {
                ModelState.AddModelError(nameof(CreateTagDto.Name), "Tag is allready exist");
                return View(tagDto);
            }

            _service.CreateAsync(tagDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int Id)
        {
            var tag = await _service.GetbyIdAsync(Id);
            if (tag == null) throw new Exception("Tag is not found!");

            UpdateTagDto tagdto = new() { Name = tag.Name };
            return View(tagdto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, UpdateTagDto tagdto)
        {
            if (!ModelState.IsValid)
            {
                return View(tagdto);
            }

            var result = await _service.AnyAsync(c => c.Name.Trim() == tagdto.Name.Trim() && c.Id != Id);
            if (result)
            {
                ModelState.AddModelError(nameof(UpdateTagDto.Name), "Tag is allready exist");
                return View(tagdto);
            }
            _service.UpdateAsync(Id, tagdto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
