using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Abstraction.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetAllAsync(int page, int take);
        Task<JobContact> GetbyIdAsync(int id);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
        Task<ContactDto> Detail(int? Id);
    }
}
