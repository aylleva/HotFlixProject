using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using HotFlix.Domain.Utilities.Extentions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Persistence.Implementations.Services
{
    internal class ActorService : IActorService
    {
        private readonly IActorRepository _repository;
        private readonly IWebHostEnvironment _env;
        public readonly string imageroot = Path.Combine("assets", "img", "bg");

        public ActorService(IActorRepository repository,IWebHostEnvironment env)
        {
          _repository = repository;
           _env = env;
        }
        public async Task<IEnumerable<GetActorDto>> GetAllAsync(int page,int take,string? search)
        {
            IEnumerable<Actor> actors = await _repository.GetAll( skip: (page - 1) * take,take:take).ToListAsync();

            if (search is not null)
            {
                actors = await _repository.GetAll(a => a.FullName.Contains(search), null, false, false, false, ((page - 1) * take), take).ToListAsync();
            }
            IEnumerable<GetActorDto> actorDto = actors.Select(c => new GetActorDto
            {
                Id = c.Id,
                FullName = c.FullName,
                MovieCount = c.MovieCount,
                Age = c.Age
            });

            return actorDto;
        }

        public Task<Actor> GetbyIdAsync(int id)
        {
            return _repository.GetbyIdAsync(id);
        }
    
        public async Task CreateAsync(CreateActorDto actorDto)
        {
            Actor actor = new Actor() { 
            
            Image=await actorDto.Image.CreateFileAsync(_env.WebRootPath,imageroot),
            FullName=actorDto.FullName,
            MovieCount=actorDto.MovieCount,
            Age=actorDto.Age
            };

            await _repository.AddAsync(actor);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, UpdateActorDto actorDto)
        {
            var existed = await _repository.GetbyIdAsync(id);
            if (existed == null) throw new Exception("Actor was not found");

            if (actorDto.Image is not null)
            {
                existed.Image = await actorDto.Image.CreateFileAsync(_env.WebRootPath, imageroot);
            }

            existed.FullName = actorDto.FullName;
            existed.Age=actorDto.Age.Value;
            existed.MovieCount=actorDto.MovieCount.Value;

             _repository.Update(existed);
            await _repository.SaveChangesAsync();   
        }
        public async Task DeleteAsync(int id)
        {
             var actor = await _repository.GetbyIdAsync(id);
            if (actor is null) throw new Exception("Not Found");

            _repository.Delete(actor);
            await _repository.SaveChangesAsync();
        }
        public Task<bool> AnyAsync(Expression<Func<Actor, bool>>? expression)
        {
            return _repository.AnyAsync(expression);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.Count();
        }

    }
}
