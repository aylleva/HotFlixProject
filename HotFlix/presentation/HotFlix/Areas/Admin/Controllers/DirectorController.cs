using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace HotFlix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DirectorController : Controller
    {
        private readonly IDirectorService _service;

        public DirectorController(IDirectorService service)
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
        public async Task<IActionResult> Create(CreateDirectorDto directordto)
        {
            if (!ModelState.IsValid)
            {
                return View(directordto);
            }

            var result = await _service.AnyAsync(c => c.FullName == directordto.Name);
            if (result)
            {
                ModelState.AddModelError(nameof(CreateDirectorDto.Name), "Director is allready exist");
                return View(directordto);
            }

            _service.CreateAsync(directordto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int Id)
        {
            var director = await _service.GetbyIdAsync(Id);
            if (director == null) throw new Exception("Director is not found!");

            UpdateDirectorDto directordto = new() { Name = director.FullName };
            return View(directordto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, UpdateDirectorDto directordto)
        {
            if (!ModelState.IsValid)
            {
                return View(directordto);
            }

            var result = await _service.AnyAsync(c => c.FullName.Trim() == directordto.Name.Trim() && c.Id != Id);
            if (result)
            {
                ModelState.AddModelError(nameof(UpdateDirectorDto.Name), "Director is allready exist");
                return View(directordto);
            }
            _service.UpdateAsync(Id, directordto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
