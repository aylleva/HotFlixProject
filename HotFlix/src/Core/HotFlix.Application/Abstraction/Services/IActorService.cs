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
    public interface IActorService
    {
        Task<IEnumerable<GetActorDto>> GetAllAsync();

        Task<Actor> GetbyIdAsync(int id);

        Task CreateAsync(CreateActorDto actorDto);

        Task UpdateAsync(int id, UpdateCategoryDto categoryDto);

        Task DeleteAsync(int id);
        Task<bool> AnyAsync(Expression<Func<Category, bool>>? expression);
    }
}
