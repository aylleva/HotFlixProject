
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;


namespace HotFlix.Application.Abstraction.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<GetMovieDto>> GetAll(int? page, int? take, string? search);

        Task<Movie> GetByIdAsync(int id);

        Task<CreateMovieDto> GetRelationalDatas();
        Task CreateAsync(CreateMovieDto movieDto);

        Task UpdateAsync(int Id, UpdateMovieDto movieDto);

        Task DeleteAsync(int Id);
        ICollection<T> GetDatas<T>() where T : BaseEntity;
        Task<int> CountAsync();
        Task ChangeStatus(int Id);
        Task<UpdateMovieDto> ExistedMovie(int Id);
    }
}
