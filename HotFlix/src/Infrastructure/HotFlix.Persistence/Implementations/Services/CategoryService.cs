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
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<GetCategoryDto>> GetAllAsync()
        {
            IEnumerable<Category> categories = await _repository.GetAll(includes:nameof(Category.Movies)).ToListAsync();

            IEnumerable<GetCategoryDto> categorydto = categories.Select(c => new GetCategoryDto { 
            Id = c.Id,
            Name = c.Name,
            MovieCount=c.Movies.Count,
            });

            return categorydto;
        }

        public Task<Category> GetbyIdAsync(int id)
        {
            return _repository.GetbyIdAsync(id);
        }

        public async Task CreateAsync(CreateCategoryDto categoryDto)
        {
            Category category = new Category() { 
            Name= categoryDto.Name
            };
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto categoryDto)
        {
            Category category = await _repository.GetbyIdAsync(id);
            if (category is null) throw new Exception("Not Found");

            category.Name = categoryDto.Name;

             _repository.Update(category);
            await _repository.SaveChangesAsync();   
        }
        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetbyIdAsync(id);
            if (category is null) throw new Exception("Not Found");

            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<Category, bool>>? expression)
        {
          return _repository.AnyAsync(expression);
        }
    }
}
