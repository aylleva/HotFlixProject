using HotFlix.Application.Abstraction.Repositories.Generic;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;


namespace HotFlix.Application.Abstraction.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<CreateMovieDto> CreateMovie();
        ICollection<T> GetDatas<T>() where T : BaseEntity;
    }
}
