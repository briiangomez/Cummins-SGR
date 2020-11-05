using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRBlazorApp.Interfaces
{
    interface ISgrService<T>
    {
        Task<List<T>> GetAllAsync(string requestUri);
        Task<T> GetByIdAsync(string requestUri, Guid Id);
        Task<T> SaveAsync(string requestUri, T obj);
        Task<T> UpdateAsync(string requestUri, Guid Id, T obj);
        Task<bool> DeleteAsync(string requestUri, Guid Id);
    }
}
