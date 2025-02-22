using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using HotFlix.Domain.Utilities.Extentions;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace HotFlix.Persistence.Implementations.Services
{
    internal class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        public readonly string imageroot = Path.Combine("assets", "img", "covers");
        public readonly string videoroot = Path.Combine("assets", "videos");
        public MovieService(IMovieRepository repository,IWebHostEnvironment env,AppDbContext context)
        {
            _env=env;
           _context = context;
            _repository = repository;
            
        }

        public async Task<IEnumerable<GetMovieDto>> GetAll(int page, int take)
        {
            IEnumerable<Movie> movies = await _repository.GetAll(null, null, false, false, false, (page - 1) * take, take, nameof(Movie.Category), nameof(Movie.Reviews)).ToListAsync();

            IEnumerable<GetMovieDto> moviesDto = movies.Select(c => new GetMovieDto
            {
                Id = c.Id,
                Name = c.Name,
                Rating = c.Rating,
                Status = c.Status,
                CategoryName = c.Category.Name,
                Views = c.Reviews.Count(),
                CreatedAt =c.CreatedAt
            });

            return moviesDto;
        }

        public Task<Movie> GetByIdAsync(int id)
        {
            return _repository.GetbyIdAsync(id,nameof(Movie.Category),nameof(Movie.Director),nameof(Movie.Country),
                nameof(Movie.MovieActors),nameof(Movie.MovieTags));
        }

        public async Task<CreateMovieDto> GetRelationalDatas()
        {
            return await _repository.CreateMovie();
        }
        public async Task CreateAsync(CreateMovieDto movieDto)
        {
            movieDto.Categories = _repository.GetDatas<Category>();
            movieDto.Tags=_repository.GetDatas<Tag>().ToList();
            movieDto.Directors = _repository.GetDatas<Director>();
            movieDto.Actors = _repository.GetDatas<Actor>().ToList();
            movieDto.Countries = _repository.GetDatas<Country>();

            Movie movie = new Movie()
            {

                Name = movieDto.Name,
                Rating = movieDto.Rating,
                Premiere = movieDto.Premiere,
                RunningTime = movieDto.RunningTime,
                Description = movieDto.Description,
                Status = false,
                CategoryId = movieDto.CategoryId,
                DirectorId = movieDto.DirectorId,
                CountryId = movieDto.CountryId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                Image = await movieDto.Image.CreateFileAsync(_env.WebRootPath, imageroot),
                TrailerUrl = await movieDto.TrailerUrl.CreateFileAsync(_env.WebRootPath, videoroot),
                VideoUrl= await movieDto.TrailerUrl.CreateFileAsync(_env.WebRootPath, videoroot)
            };

            if (movieDto.TagIds != null)
            {
                movie.MovieTags = movieDto.TagIds.Select(t => new MovieTags { TagId = t }).ToList();
            }
            if (movieDto.ActorIds != null)
            {
                movie.MovieActors = movieDto.ActorIds.Select(t => new MovieActors { ActorId = t }).ToList();
            }

            await _repository.AddAsync(movie);
            await _repository.SaveChangesAsync();

        }

        public async Task<UpdateMovieDto> ExistedMovie(int Id)
        {
            var movie=await _repository.GetbyIdAsync(Id,nameof(Movie.Category), nameof(Movie.Director), nameof(Movie.Country),
                nameof(Movie.MovieActors), nameof(Movie.MovieTags));
            if (movie == null) throw new Exception("Not Found");

            UpdateMovieDto movieDto = new UpdateMovieDto()
            {
                Name = movie.Name,
                Rating = movie.Rating,
                Premiere = movie.Premiere,
                RunningTime = movie.RunningTime,
                Description = movie.Description,
                Categories = _repository.GetDatas<Category>(),
                CategoryId=movie.CategoryId,
                Directors = _repository.GetDatas<Director>(),
                DirectorId=movie.DirectorId,
                Countries = _repository.GetDatas<Country>(),
                CountryId=movie.CountryId,

                Tags=_repository.GetDatas<Tag>().ToList(),
                Actors = _repository.GetDatas<Actor>().ToList(),
                TagIds=movie.MovieTags.Select(t => t.TagId).ToList(),
                ActorIds=movie.MovieActors.Select(t => t.ActorId).ToList()

            };
            return movieDto;
        }
        public async Task UpdateAsync(int Id, UpdateMovieDto movieDto)
        {
            Movie existed=await _repository.GetbyIdAsync(Id, nameof(Movie.Category), nameof(Movie.Director), nameof(Movie.Country),
                nameof(Movie.MovieActors), nameof(Movie.MovieTags));
            if (existed == null) throw new Exception("Movie Was Not Found");

            movieDto.Tags = _repository.GetDatas<Tag>().ToList();
            movieDto.Actors = _repository.GetDatas<Actor>().ToList();

            if (movieDto.TagIds is null)
            {
                movieDto.TagIds = new();
            }
            _context.MovieTags.RemoveRange(existed.MovieTags
              .Where(pc => !movieDto.TagIds
              .Exists(cI => cI == pc.TagId)).ToList());

            _context.MovieTags.AddRange(movieDto.TagIds
            .Where(cI => !existed.MovieTags
            .Exists(pc => pc.TagId == cI))
            .Select(c => new MovieTags { TagId = c, MovieId = existed.Id }).ToList());

            if (movieDto.ActorIds is null)
            {
                movieDto.ActorIds = new();
            }
            _context.MoviesActors.RemoveRange(existed.MovieActors
              .Where(pc => !movieDto.ActorIds
              .Exists(cI => cI == pc.ActorId)).ToList());

            _context.MoviesActors.AddRange(movieDto.ActorIds
            .Where(cI => !existed.MovieActors
            .Exists(pc => pc.ActorId == cI))
            .Select(c => new MovieActors { ActorId = c, MovieId = existed.Id }).ToList());

            if(movieDto.Image is not null)
            {
                existed.Image =await movieDto.Image.CreateFileAsync(_env.WebRootPath, imageroot);
            } 

            if(movieDto.TrailerUrl is not null)
            {
                existed.TrailerUrl = await movieDto.TrailerUrl.CreateFileAsync(_env.WebRootPath, videoroot);
            }
            if (movieDto.VideoUrl is not null)
            {
                existed.VideoUrl = await movieDto.VideoUrl.CreateFileAsync(_env.WebRootPath, videoroot);
            }

            existed.Name = movieDto.Name;
            existed.Rating=movieDto.Rating.Value;
            existed.Premiere=movieDto.Premiere.Value;
            existed.RunningTime = movieDto.RunningTime;
            existed.Description = movieDto.Description;
            existed.CategoryId=movieDto.CategoryId.Value;
            existed.DirectorId=movieDto.DirectorId.Value;
            existed.CountryId=movieDto.CountryId.Value;

            _repository.Update(existed);
            await _repository.SaveChangesAsync();
        }


        public async Task DeleteAsync(int Id)
        {
            Movie movie = await _repository.GetbyIdAsync(Id);
            if (movie is null) throw new Exception("Not Found");

            _repository.Delete(movie);
            await _repository.SaveChangesAsync();
        }

        public ICollection<T> GetDatas<T>() where T : BaseEntity
        {
           return _repository.GetDatas<T>();
        }

        public async Task<int> CountAsync()
        {
            return await _repository.Count();
        }

        public async Task ChangeStatus(int Id)
        {
            var movie = await _repository.GetbyIdAsync(Id);
            if (movie is null) throw new Exception("movie Was Not Found");

            if (movie.Status==false)
            {
                movie.Status = true;

            }
            else
            {
                movie.Status = false;
            }

             _repository.Update(movie);
            await _repository.SaveChangesAsync();
        }
    }
}
