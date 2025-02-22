using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using HotFlix.Persistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotFlix.Persistence.Implementations.Repositories
{
    internal class MovieRepository:Repository<Movie>,IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CreateMovieDto> CreateMovie()
        {
            CreateMovieDto moviedto = new CreateMovieDto
            {
                Categories = await _context.Categories.ToListAsync(),
                Tags = await _context.Tags.ToListAsync(),
                Actors = await _context.Actors.ToListAsync(),
                Directors = await _context.Directors.ToListAsync(),
                Countries = await _context.Countries.ToListAsync()
            };
            return moviedto;
        }

        public ICollection<T> GetDatas<T>()where T : BaseEntity
        {
            return  _context.Set<T>().ToList();
        }
    }
}
