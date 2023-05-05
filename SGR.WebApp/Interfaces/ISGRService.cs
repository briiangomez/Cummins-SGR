using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBBlazorApp.Interfaces
{
    interface ISGRService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid Id);
        Task<T> SaveAsync(T obj);
        Task<T> UpdateAsync(Guid Id, T obj);
        Task<bool> DeleteAsync(Guid Id);
    }
}
