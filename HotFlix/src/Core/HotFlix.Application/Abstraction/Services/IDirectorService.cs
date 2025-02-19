
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
    public interface IDirectorService
    {
        Task<IEnumerable<GetDirectorDto>> GetAllAsync();

        Task<Director> GetbyIdAsync(int id);

        Task CreateAsync(CreateDirectorDto directorDto);

        Task UpdateAsync(int id, UpdateDirectorDto directorDto);

        Task DeleteAsync(int id);
        Task<bool> AnyAsync(Expression<Func<Director, bool>>? expression);
    }
}
