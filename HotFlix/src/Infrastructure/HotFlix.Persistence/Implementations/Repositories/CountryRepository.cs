using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using HotFlix.Persistence.Implementations.Repositories.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Persistence.Implementations.Repositories
{
    internal class CountryRepository:Repository<Country>,ICountryRepository
    {
        public CountryRepository(AppDbContext context) : base(context) { }
        
    }
}
