using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace HotFlix.Persistence.Implementations.Services
{
    internal class PremiumService : IPremiumService
    {
        private readonly IPremiumPlanRepository _repository;

        public PremiumService(IPremiumPlanRepository repository)
        {
           _repository = repository;
        }

        public async Task<IEnumerable<GetPremiumPlanDto>> GetAllAsync()
        {
            IEnumerable<PremiumPlan> plans = await _repository.GetAll(includes: nameof(PremiumPlan.Users)).ToListAsync();

            IEnumerable<GetPremiumPlanDto> plansDto = plans.Select(c => new GetPremiumPlanDto
            {
                Id = c.Id,
               PlanName = c.PlanName,
               Quality = c.Quality,
               Price = c.Price,
               Expires = c.Expires,
               UserCount=c.Users.Count
            });

            return plansDto;
        }

        public Task<PremiumPlan> GetbyIdAsync(int id)
        {
            return _repository.GetbyIdAsync(id);
        }
        public async Task CreateAsync(CreatePremiumPlanDto premiumDto)
        {
            PremiumPlan plan = new PremiumPlan()
            {
                PlanName = premiumDto.PlanName,
                Quality = premiumDto.Quality,
                Price = premiumDto.Price,
                Expires = premiumDto.Expires
            };
            await _repository.AddAsync(plan);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdatePremiumPlanDto premiumDto)
        {
            PremiumPlan plan = await _repository.GetbyIdAsync(id);
            if (plan is null) throw new Exception("Not Found");

            plan.PlanName = premiumDto.PlanName;
            plan.Quality = premiumDto.Quality;
            plan.Price = premiumDto.Price.Value;
            plan.Expires = premiumDto.Expires;

            _repository.Update(plan);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            PremiumPlan plan = await _repository.GetbyIdAsync(id);
            if (plan is null) throw new Exception("Not Found");

             _repository.Delete(plan);
            await _repository.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<PremiumPlan, bool>>? expression)
        {
            return _repository.AnyAsync(expression);
        }
    }
}
