using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using HotFlix.Persistence.Implementations.Repositories.Generic;


namespace HotFlix.Persistence.Implementations.Repositories
{
    internal class TagRepository:Repository<Tag>,ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context) { }
        
    }
}
