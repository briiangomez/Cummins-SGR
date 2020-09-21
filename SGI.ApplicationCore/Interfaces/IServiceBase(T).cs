using SGI.ApplicationCore.DTOs;
using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGI.ApplicationCore.Interfaces
{
    public interface IServiceBase<T> where T : BaseEntity
    {
        T Get(Guid id, List<string> include = null);

        Task<T> GetAsync(Guid id, List<string> include = null);

        PagedResult<T> GetPaged(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null);

        Task<PagedResult<T>> GetPagedAsync(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null);

        T[] GetAll(List<string> include = null, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null, bool desc = false);

        Task<T[]> GetAllAsync(List<string> include = null, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null, bool desc = false);

        Guid Insert(T entity);

        Task<Guid> InsertAsync(T entity);

        void Update(Guid id, T entity);

        Task UpdateAsync(Guid id, T entity);

        void Delete(Guid id);

        Task DeleteAsync(Guid id);

        int Count();


    }
}
