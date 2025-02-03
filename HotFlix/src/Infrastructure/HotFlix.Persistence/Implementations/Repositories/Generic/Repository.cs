using HotFlix.Application.Abstraction.Repositories.Generic;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Persistence.Implementations.Repositories.Generic
{
    internal class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }
        public IQueryable<T> GetAll(
            Expression<Func<T, bool>>? 
            expression = null,
            Expression<Func<T, object>>? sort = null, 
            bool IsDescending = false, bool IsTracking = false, 
            bool ignoreFilter = false, int skip = 0, 
            int take = 0,
            params string[]? includes)
        {
            IQueryable<T> query = _table;

            if (expression is not null) query = query.Where(expression);

            if (includes is not null)
            {
                query = getincludes(query, includes);
            }

            if (sort is not null) query = IsDescending ? query.OrderByDescending(sort) : query.OrderBy(sort);

            query = query.Skip(skip);

            if (take != 0) query = query.Take(take);

            if (ignoreFilter) query = query.IgnoreQueryFilters();

            return IsTracking ? query : query.AsNoTracking();

        }

        public Task<int> SaveChangesAsync()
        {
           return _context.SaveChangesAsync();
        }

        private IQueryable<T> getincludes(IQueryable<T> query, params string[]? includes)
        {
            for (int i = 0; i < includes.Length; i++)
            {
                query = query.Include(includes[i]);
            }
            return query;
        }
    }
}
