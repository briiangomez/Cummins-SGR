using SGI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGI.DAL.Contracts
{
    public interface IManager<T>
    {
        Task<int> Create(T item);
        Task<int> Delete(int Id);
        Task<int> Count(string search);
        Task<int> Update(T item);
        Task<T> GetById(int Id);
        Task<List<T>> ListAll();
        Task<List<T>> ListAll(int skip, int take, string orderBy, string direction, string search);
        
    }
}
