using HotFlix.Domain.Models;
using System.Linq.Expressions;


namespace HotFlix.Application.Abstraction.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? sort = null,
            bool IsDescending = false,
            bool IsTracking = false,
            bool ignoreFilter = false,
            int skip = 0,
            int take = 0,
            params string[]? includes);

        Task<T> GetbyIdAsync(int id, params string[] includes);

        Task AddAsync(T entity);

        void Delete(T entity);

        void Update(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>>? expression);
        Task<int> SaveChangesAsync();

        Task<int> Count();
       
    }
}
