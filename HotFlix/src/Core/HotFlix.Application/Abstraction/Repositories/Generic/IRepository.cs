using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Abstraction.Repositories.Generic
{
    public interface IRepository<T> where T :BaseEntity, new()
    {
       IQueryable<T> GetAll(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? sort = null,
            bool IsDescending = false,
            bool IsTracking = false,
            bool ignoreFilter = false,
            int skip = 0,
            int take = 0,
            params string[]? includes
           );
        Task<int> SaveChangesAsync();
    }
}
