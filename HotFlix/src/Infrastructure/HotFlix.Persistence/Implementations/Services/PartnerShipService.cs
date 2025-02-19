using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Persistence.Implementations.Services
{
    internal class PartnerShipService:IPartnerShipService
    {
        private readonly IPartnerShipRepository _repository;

        public PartnerShipService(IPartnerShipRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetPartnerShipDto>> GetAllAsync()
        {
            IEnumerable<PartnerShip> partnerships = await _repository.GetAll().ToListAsync();

            IEnumerable<GetPartnerShipDto> partnershipDto = partnerships.Select(c => new GetPartnerShipDto
            {
                Id = c.Id,
                Name = c.Name
              
            });

            return partnershipDto;
        }

        public Task<PartnerShip> GetbyIdAsync(int id)
        {
            return _repository.GetbyIdAsync(id);
        }

        public async Task CreateAsync(CreatePartnerShipDto partnerDto)
        {
            PartnerShip partnership = new PartnerShip()
            {
                Name = partnerDto.Name
            };
            await _repository.AddAsync(partnership);
            await _repository.SaveChangesAsync();
        }
      
        public async Task UpdateAsync(int id, UpdatePartnerShipDto partnerDto)
        {
           PartnerShip partnership = await _repository.GetbyIdAsync(id);
            if (partnership is null) throw new Exception("Not Found");

            partnership.Name = partnerDto.Name;
            _repository.Update(partnership);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            PartnerShip partnership = await _repository.GetbyIdAsync(id);
            if (partnership is null) throw new Exception("Not Found");

            _repository.Delete(partnership);
            await _repository.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<PartnerShip, bool>>? expression)
        {
            return _repository.AnyAsync(expression);
        }
    }
}
