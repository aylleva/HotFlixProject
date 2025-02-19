using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Persistence.Implementations.Services
{
    internal class DirectorService : IDirectorService
    {
        private readonly IDirectorRepository _repository;

        public DirectorService(IDirectorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetDirectorDto>> GetAllAsync()
        {
            IEnumerable<Director> directors = await _repository.GetAll(includes: nameof(Country.Movies)).ToListAsync();

            IEnumerable<GetDirectorDto> directorDto = directors.Select(c => new GetDirectorDto
            {
                Id = c.Id,
                Name = c.FullName,
                MovieCount = c.Movies.Count,
            });

            return directorDto;
        }
        
        public Task<Director> GetbyIdAsync(int id)
        {
            return _repository.GetbyIdAsync(id);
        }

        public async Task CreateAsync(CreateDirectorDto directorDto)
        {
            Director director = new Director()
            {
                FullName = directorDto.Name
            };
            await _repository.AddAsync(director);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Director director = await _repository.GetbyIdAsync(id);
            if (director is null) throw new Exception("Not Found");

            _repository.Delete(director);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateDirectorDto directorDto)
        {
            Director director = await _repository.GetbyIdAsync(id);
            if (director is null) throw new Exception("Not Found");

            director.FullName = directorDto.Name;
            _repository.Update(director);
            await _repository.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<Director, bool>>? expression)
        {
            return _repository.AnyAsync(expression);
        }
    }
}
