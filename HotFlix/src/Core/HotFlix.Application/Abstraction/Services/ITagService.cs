
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
    public interface ITagService
    {
        Task<IEnumerable<GetTagDto>> GetAllAsync();

        Task<Tag> GetbyIdAsync(int id);

        Task CreateAsync(CreateTagDto tagDto);

        Task UpdateAsync(int id, UpdateTagDto tagDto);

        Task DeleteAsync(int id);
        Task<bool> AnyAsync(Expression<Func<Tag, bool>>? expression);
    }
}
