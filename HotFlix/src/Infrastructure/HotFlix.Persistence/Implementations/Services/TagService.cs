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
    internal class TagService:ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetTagDto>> GetAllAsync()
        {
            IEnumerable<Tag> tags = await _repository.GetAll(includes:nameof(Tag.MovieTags)).ToListAsync();

            IEnumerable<GetTagDto> tagDto = tags.Select(c => new GetTagDto
            {
                Id = c.Id,
                Name = c.Name,
                MovieCount=c.MovieTags.Count
            });

            return tagDto;
        }

        public Task<Tag> GetbyIdAsync(int id)
        {
            return _repository.GetbyIdAsync(id);
        }
        public async Task CreateAsync(CreateTagDto tagDto)
        {
            Tag tag = new Tag()
            {
                Name = tagDto.Name
            };
            await _repository.AddAsync(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateTagDto tagDto)
        {
            Tag tag = await _repository.GetbyIdAsync(id);
            if (tag is null) throw new Exception("Not Found");

           tag.Name = tagDto.Name;
            _repository.Update(tag);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetbyIdAsync(id);
            if (tag is null) throw new Exception("Not Found");

            _repository.Delete(tag);
            await _repository.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<Tag, bool>>? expression)
        {
            return _repository.AnyAsync(expression);
        }
    }
}
