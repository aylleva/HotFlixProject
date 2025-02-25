using Azure;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Enums;
using HotFlix.Domain.Utilities.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotFlix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ActorController : Controller
    {
        private readonly IActorService _service;

        public ActorController(IActorService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(string? search, int page = 1, int take = 10)
        {
            if (page < 1) throw new Exception("Page was not found");

            int count = await _service.CountAsync();
            double total = Math.Ceiling((double)count / 10);

            if (page > total) throw new Exception("Page was not found");
           
            PaginatedItemDto<GetActorDto> actorDto = new()
            {
                CurrectPage = page,
                TotalPage = total,
                Items = _service.GetAllAsync(page, take,search),
                Search = search
            };
            return View(actorDto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateActorDto actorDto)
        {
            if (!ModelState.IsValid)
            {
                return View(actorDto);
            }

            if (!actorDto.Image.CheckType("image/"))
            {
                ModelState.AddModelError(nameof(actorDto.Image), "Wrong Type!");
                return View(actorDto);
            }
            if (!actorDto.Image.CheckFileSize(FileSize.MB, 2))
            {

                ModelState.AddModelError(nameof(actorDto.Image), "Wrong Size!");
                return View(actorDto);
            }

            await _service.CreateAsync(actorDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? Id)
        {
            var actor = await _service.GetbyIdAsync(Id.Value);
            if (actor == null) throw new Exception("Actor is not found!");

            UpdateActorDto actorDto = new UpdateActorDto
            {
                FullName = actor.FullName,
                MovieCount = actor.MovieCount,
                Age = actor.Age,
                ExistedImage=actor.Image
            };

            return View(actorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? Id, UpdateActorDto actorDto)
        {
            if (!ModelState.IsValid)
            {
                return View(actorDto);
            }

            if(actorDto.Image is not null)
            {
                if (!actorDto.Image.CheckType("image/"))
                {
                    ModelState.AddModelError(nameof(actorDto.Image), "Wrong Type!");
                    return View(actorDto);
                }
                if (!actorDto.Image.CheckFileSize(FileSize.MB, 2))
                {

                    ModelState.AddModelError(nameof(actorDto.Image), "Wrong Size!");
                    return View(actorDto);
                }
            }

            await _service.UpdateAsync(Id.Value,actorDto);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
