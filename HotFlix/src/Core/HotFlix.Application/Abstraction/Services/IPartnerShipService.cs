
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Abstraction.Services
{
    public interface IPartnerShipService
    {
        Task<IEnumerable<GetPartnerShipDto>> GetAllAsync();

        Task<PartnerShip> GetbyIdAsync(int id);

        Task CreateAsync(CreatePartnerShipDto partnerDto);

        Task UpdateAsync(int id, UpdatePartnerShipDto partnerDto);

        Task DeleteAsync(int id);
        Task<bool> AnyAsync(Expression<Func<PartnerShip, bool>>? expression);
    }
}
