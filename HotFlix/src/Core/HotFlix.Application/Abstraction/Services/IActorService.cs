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
        Task<IEnumerable<GetActorDto>> GetAllAsync(int page,int take, string? search);

        Task<Actor> GetbyIdAsync(int id);

        Task CreateAsync(CreateActorDto actorDto);

        Task UpdateAsync(int id, UpdateActorDto actorDto);

        Task DeleteAsync(int id);
        Task<bool> AnyAsync(Expression<Func<Actor, bool>>? expression);
        Task<int> CountAsync();
    }
}
