using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using System;
using System.Linq.Expressions;

namespace HotFlix.Application.Abstraction.Services
{
    public interface IPremiumService
    {
        Task<IEnumerable<GetPremiumPlanDto>> GetAllAsync();

        Task<PremiumPlan> GetbyIdAsync(int id);

        Task CreateAsync(CreatePremiumPlanDto premiumDto);

        Task UpdateAsync(int id, UpdatePremiumPlanDto premiumDto);

        Task DeleteAsync(int id);
        Task<bool> AnyAsync(Expression<Func<PremiumPlan, bool>>? expression);
    }
}
