using AspNetCoreGeneratedDocument;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Enums;
using HotFlix.Domain.Models;
using HotFlix.Domain.Utilities.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HotFlix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class MovieController : Controller
    {
        private readonly IMovieService _service;
        public MovieController(IMovieService service)
        {
            _service = service;

        }
        public async Task<IActionResult> Index(int page = 1, int take = 10)
        {
            if (page < 1) throw new Exception("Page was not found");

            int count = await _service.CountAsync();
            double total = Math.Ceiling((double)count / 10);

            if (page > total) throw new Exception("Page was not found");

            PaginatedItemDto<GetMovieDto> movieDto = new()
            {
                CurrectPage = page,
                TotalPage = total,
                Items = _service.GetAll(page, take),
            };
            return View(movieDto);
        }

        public async Task<IActionResult> Create()
        {
            return View(await _service.GetRelationalDatas());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieDto movieDto)
        {
            movieDto.Categories = _service.GetDatas<Category>();
            movieDto.Tags = _service.GetDatas<Tag>().ToList();
            movieDto.Directors = _service.GetDatas<Director>();
            movieDto.Countries = _service.GetDatas<Country>();
            movieDto.Actors = _service.GetDatas<Actor>().ToList();


            if (!ModelState.IsValid)
            {
                return View(movieDto);
            }



            if (!movieDto.Image.CheckType("image/"))
            {
                ModelState.AddModelError(nameof(movieDto.Image), "Wrong Type!");
                return View(movieDto);
            }
            if (!movieDto.Image.CheckFileSize(FileSize.MB, 2))
            {

                ModelState.AddModelError(nameof(movieDto.Image), "Wrong Size!");
                return View(movieDto);
            }

            if (!movieDto.TrailerUrl.CheckType("video/"))
            {
                ModelState.AddModelError(nameof(movieDto.TrailerUrl), "Wrong Type!");
                return View(movieDto);
            }
            if (!movieDto.TrailerUrl.CheckFileSize(FileSize.MB, 512))
            {

                ModelState.AddModelError(nameof(movieDto.TrailerUrl), "Wrong Size!");
                return View(movieDto);
            }

            if (!movieDto.VideoUrl.CheckType("video/"))
            {
                ModelState.AddModelError(nameof(movieDto.VideoUrl), "Wrong Type!");
                return View(movieDto);
            }
            if (!movieDto.VideoUrl.CheckFileSize(FileSize.MB, 512))
            {

                ModelState.AddModelError(nameof(movieDto.VideoUrl), "Wrong Size!");
                return View(movieDto);
            }
            bool result = _service.GetDatas<Category>().Any(c=>c.Id==movieDto.CategoryId);
            if (!result)
            {
                ModelState.AddModelError(nameof(CreateMovieDto.CategoryId), "Category does not exists!");
                return View(movieDto);
            }

            bool result2 = _service.GetDatas<Director>().Any(c => c.Id == movieDto.DirectorId);
            if (!result2)
            {
                ModelState.AddModelError(nameof(CreateMovieDto.DirectorId), "Director does not exists!");
                return View(movieDto);
            }
            bool result3 = _service.GetDatas<Country>().Any(c => c.Id == movieDto.CountryId);
            if (!result3)
            {
                ModelState.AddModelError(nameof(CreateMovieDto.CountryId), "Country does not exists!");
                return View(movieDto);
            }
            if (movieDto.TagIds is not null)
            {
                bool tagresult = movieDto.TagIds.Any(t => !movieDto.Tags.Exists(pt => pt.Id == t));
                if (tagresult)
                {
                    ModelState.AddModelError(nameof(CreateMovieDto.TagIds), "Incorrect");
                    return View(movieDto);
                }
            }
            if (movieDto.ActorIds is not null)
            {
                bool actorresult = movieDto.ActorIds.Any(t => !movieDto.Actors.Exists(pt => pt.Id == t));
                if (actorresult)
                {
                    ModelState.AddModelError(nameof(CreateMovieDto.ActorIds), "Incorrect");
                    return View(movieDto);
                }
            }
            await _service.CreateAsync(movieDto);
            return RedirectToAction(nameof(Index));



        }

        public async Task<IActionResult> Update(int? Id)
        {
            if (Id is null || Id < 1) return BadRequest(); 

            return View(await _service.ExistedMovie(Id.Value));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? Id,UpdateMovieDto movieDto)
        {
            if (Id is null || Id < 1) return BadRequest();

            var existed=await _service.GetByIdAsync(Id.Value);
            
            movieDto.Categories = _service.GetDatas<Category>();
            movieDto.Tags = _service.GetDatas<Tag>().ToList();
            movieDto.Directors = _service.GetDatas<Director>();
            movieDto.Countries = _service.GetDatas<Country>();
            movieDto.Actors = _service.GetDatas<Actor>().ToList();

            if (!ModelState.IsValid)
            {
                return View(movieDto);
            }

           if(movieDto.Image is not null)
            {
                if (!movieDto.Image.CheckType("image/"))
                {
                    ModelState.AddModelError(nameof(movieDto.Image), "Wrong Type!");
                    return View(movieDto);
                }
                if (!movieDto.Image.CheckFileSize(FileSize.MB, 2))
                {

                    ModelState.AddModelError(nameof(movieDto.Image), "Wrong Size!");
                    return View(movieDto);
                }
            }

           if(movieDto.TrailerUrl is not null)
            {
                if (!movieDto.TrailerUrl.CheckType("video/"))
                {
                    ModelState.AddModelError(nameof(movieDto.TrailerUrl), "Wrong Type!");
                    return View(movieDto);
                }
                if (!movieDto.TrailerUrl.CheckFileSize(FileSize.MB, 512))
                {

                    ModelState.AddModelError(nameof(movieDto.TrailerUrl), "Wrong Size!");
                    return View(movieDto);
                }
            }

           if(movieDto.VideoUrl is not null)
            {
                if (!movieDto.TrailerUrl.CheckType("video/"))
                {
                    ModelState.AddModelError(nameof(movieDto.TrailerUrl), "Wrong Type!");
                    return View(movieDto);
                }
                if (!movieDto.TrailerUrl.CheckFileSize(FileSize.MB, 512))
                {

                    ModelState.AddModelError(nameof(movieDto.TrailerUrl), "Wrong Size!");
                    return View(movieDto);
                }
            }

            if (existed.CategoryId != movieDto.CategoryId)
            {
                bool result = _service.GetDatas<Category>().Any(c => c.Id == movieDto.CategoryId);
                if (!result)
                {
                    ModelState.AddModelError(nameof(CreateMovieDto.CategoryId), "Category does not exists!");
                    return View(movieDto);
                }
            }

            if(existed.DirectorId!= movieDto.DirectorId)
            {
                bool result2 = _service.GetDatas<Director>().Any(c => c.Id == movieDto.DirectorId);
                if (!result2)
                {
                    ModelState.AddModelError(nameof(CreateMovieDto.DirectorId), "Director does not exists!");
                    return View(movieDto);
                }
            }
            
            if(existed.CountryId!= movieDto.CountryId)
            {
                bool result3 = _service.GetDatas<Country>().Any(c => c.Id == movieDto.CountryId);
                if (!result3)
                {
                    ModelState.AddModelError(nameof(CreateMovieDto.CountryId), "Country does not exists!");
                    return View(movieDto);
                }
            }

            if (movieDto.TagIds is not null)
            {
                bool tagresult = movieDto.TagIds.Any(t => !movieDto.Tags.Exists(pt => pt.Id == t));
                if (tagresult)
                {
                    ModelState.AddModelError(nameof(CreateMovieDto.TagIds), "Incorrect");
                    return View(movieDto);
                }
            }
            if (movieDto.ActorIds is not null)
            {
                bool actorresult = movieDto.ActorIds.Any(t => !movieDto.Actors.Exists(pt => pt.Id == t));
                if (actorresult)
                {
                    ModelState.AddModelError(nameof(CreateMovieDto.ActorIds), "Incorrect");
                    return View(movieDto);
                }
            }

            await _service.UpdateAsync(Id.Value, movieDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeStatus(int Id)
        {
            await _service.ChangeStatus(Id);
            return RedirectToAction(nameof(Index));
        }

    } 
}