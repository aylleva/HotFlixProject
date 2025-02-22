
using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using HotFlix.Persistence.Implementations.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Persistence.Implementations.Repositories
{
    internal class ContactRepository:Repository<JobContact>,IContactRepository
    {
        public ContactRepository(AppDbContext context):base(context) { }
        
    }
}
