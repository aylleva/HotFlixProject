using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using HotFlix.Persistence.Implementations.Repositories.Generic;


namespace HotFlix.Persistence.Implementations.Repositories
{
    internal class MovieRepository:Repository<Movie>,IMovieRepository
    {
        public MovieRepository(AppDbContext context) : base(context) { }
       
    }
}
