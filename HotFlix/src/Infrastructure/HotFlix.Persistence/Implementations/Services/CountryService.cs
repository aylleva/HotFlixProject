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
    internal class CountryService : ICountryService
    {
        private readonly ICountryRepository _repository;

        public CountryService(ICountryRepository repository)
        {
           _repository = repository;
        }
        public async Task<IEnumerable<GetCountryDto>> GetAllAsync()
        {
            IEnumerable<Country> countries = await _repository.GetAll(includes: nameof(Country.Movies)).ToListAsync();

            IEnumerable<GetCountryDto> countryDto = countries.Select(c => new GetCountryDto
            {
                Id = c.Id,
                Name = c.Name,
                MovieCount = c.Movies.Count,
            });

            return countryDto;
        }

        public Task<Country> GetbyIdAsync(int id)
        {
            return _repository.GetbyIdAsync(id);
        }

        public async  Task CreateAsync(CreateCountryDto countryDto)
        {
            Country country = new Country()
            {
                Name = countryDto.Name
            };
            await _repository.AddAsync(country);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateCountryDto countryDto)
        {
           Country country = await _repository.GetbyIdAsync(id);
            if (country is null) throw new Exception("Not Found");

            country.Name = countryDto.Name;
            _repository.Update(country);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Country country = await _repository.GetbyIdAsync(id);
            if (country is null) throw new Exception("Not Found");

            _repository.Delete(country);
            await _repository.SaveChangesAsync();
        }
        public Task<bool> AnyAsync(Expression<Func<Country, bool>>? expression)
        {
            return _repository.AnyAsync(expression);
        }
    }
}
