using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using System.Linq.Expressions;


namespace HotFlix.Application.Abstraction.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDto>> GetAllAsync();

        Task<Category> GetbyIdAsync(int id);

        Task CreateAsync(CreateCategoryDto categoryDto);

        Task UpdateAsync(int id,UpdateCategoryDto categoryDto);

        Task DeleteAsync(int id);
        Task<bool> AnyAsync(Expression<Func<Category, bool>>? expression);
    }
}
