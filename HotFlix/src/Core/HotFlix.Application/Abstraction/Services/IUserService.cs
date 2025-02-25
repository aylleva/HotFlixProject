using HotFlix.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UsersDto>> GetAllAsync(int page, int take, string? search);
        Task DeleteAsync(string id);
        Task<int> CountAsync();

        Task ChangeStatus(string Id);
    }
}
