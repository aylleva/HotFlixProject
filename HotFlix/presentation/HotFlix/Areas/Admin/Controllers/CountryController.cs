using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotFlix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CountryController : Controller
    {
        private readonly ICountryService _service;

        public CountryController(ICountryService service)
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
        public async Task<IActionResult> Create(CreateCountryDto countryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(countryDto);
            }

            var result = await _service.AnyAsync(c => c.Name == countryDto.Name);
            if (result)
            {
                ModelState.AddModelError(nameof(CreateCountryDto.Name), "Country is allready exist");
                return View(countryDto);
            }

            _service.CreateAsync(countryDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int Id)
        {
            var country = await _service.GetbyIdAsync(Id);
            if (country == null) throw new Exception("Country is not found!");

            UpdateCountryDto countryDto = new() { Name = country.Name };
            return View(countryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, UpdateCountryDto countryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(countryDto);
            }

            var result = await _service.AnyAsync(c => c.Name.Trim() == countryDto.Name.Trim() && c.Id != Id);
            if (result)
            {
                ModelState.AddModelError(nameof(UpdateCountryDto.Name), "Country is allready exist");
                return View(countryDto);
            }
            _service.UpdateAsync(Id, countryDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
