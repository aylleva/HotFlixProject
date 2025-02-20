using HotFlix.Application.Abstraction.Repositories.Generic;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace HotFlix.Persistence.Implementations.Repositories.Generic
{
    internal class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? sort = null,
            bool IsDescending = false,
            bool IsTracking = false, 
            bool ignoreFilter = false,
            int skip = 0, int take = 0, 
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

        public async Task<T> GetbyIdAsync(int id, params string[] includes)
        {
            IQueryable<T> query = _table.Where(x=>x.Id==id);

            if (includes is not null)
            {
                query = getincludes(query, includes);
            }

            return await query.FirstOrDefaultAsync();
        }
        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>>? expression)
        {
            return _table.AnyAsync(expression);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }


        private IQueryable<T> getincludes(IQueryable<T> query, params string[]? includes)
        {
            for (int i = 0; i < includes.Length; i++)
            {
                query = query.Include(includes[i]);
            }
            return query;
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public async Task<int> Count()
        {
           return  await _table.CountAsync();
        }
    }
}
