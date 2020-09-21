using SGI.ApplicationCore.DTOs;
using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGI.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(Guid id, List<string> include = null);

        Task<T> GetAsync(Guid id, List<string> include = null);

        PagedResult<T> GetPaged(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null);

        Task<PagedResult<T>> GetPagedAsync(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null);

        T[] GetAll(List<string> include = null, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null, bool desc = false);

        Task<T[]> GetAllAsync(List<string> include = null, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null, bool desc = false);

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy, bool asNoTracking = false,
            string[] includes2 = null, bool includeDeleted = false, params Expression<Func<T, object>>[] includes);

        void Insert(T entity);

        Task InsertAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(Guid id);

        Task DeleteAsync(Guid id);

        int Count();

    }
}
