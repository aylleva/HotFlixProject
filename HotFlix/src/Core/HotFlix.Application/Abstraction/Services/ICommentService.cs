using HotFlix.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Abstraction.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllAsync(int page,int take);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
