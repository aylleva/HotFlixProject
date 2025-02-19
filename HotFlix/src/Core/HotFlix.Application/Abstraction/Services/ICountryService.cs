using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Abstraction.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<GetCountryDto>> GetAllAsync();

        Task<Country> GetbyIdAsync(int id);

        Task CreateAsync(CreateCountryDto countryDto);

        Task UpdateAsync(int id, UpdateCountryDto countryDto);

        Task DeleteAsync(int id);
        Task<bool> AnyAsync(Expression<Func<Country, bool>>? expression);
    }
}
