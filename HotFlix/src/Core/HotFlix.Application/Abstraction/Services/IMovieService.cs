

using HotFlix.Application.Dtos;

namespace HotFlix.Application.Abstraction.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<GetMovieDto>> GetAll(int page, int take);

        Task<GetMovieDto> GetByIdAsync(int id);

        Task CreateAsync(CreateMovieDto movieDto);

        Task UpdateAsync(int Id, UpdateMovieDto movieDto);

        Task DeleteAsync(int Id);
    }
}
