using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Persistence.Implementations.Services
{
    internal class ContactService : IContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
           _repository = repository;
        }
        public async Task<IEnumerable<ContactDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<JobContact> contacts = await _repository.GetAll(null, null, false, false, false, (page - 1) * take, take,nameof(JobContact.PartnerShip)).ToListAsync();

            IEnumerable<ContactDto> contactdto = contacts.Select(c => new ContactDto
            {
                Id = c.Id,
                Email=c.Email,
                Name= c.Name,
                PartnerShipName=c.PartnerShip.Name,
                Message=c.Message,
                CreatedDate= c.CreatedAt
            });

            return contactdto;
        }

        public Task<JobContact> GetbyIdAsync(int id)
        {
            return _repository.GetbyIdAsync(id,nameof(JobContact.PartnerShip));
        }

        public async Task<ContactDto> Detail(int? Id)
        {
            var contact = await _repository.GetbyIdAsync(Id.Value,nameof(JobContact.PartnerShip));
            if (contact is null) throw new Exception("Not Found");

            ContactDto contactdto = new ContactDto()
            {
                Name = contact.Name,
                Email = contact.Email,
                Message = contact.Message,
                PartnerShipName = contact.PartnerShip.Name,
                CreatedDate = contact.CreatedAt
            };
            return contactdto;
        }
        public async Task DeleteAsync(int id)
        {
          var contact=await _repository.GetbyIdAsync(id);
            if (contact == null) throw new Exception("Contact was not found!");

           _repository.Delete(contact);
            await _repository.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _repository.Count();
        }
    }
}
