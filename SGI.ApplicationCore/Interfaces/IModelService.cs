using SGI.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGI.ApplicationCore.Interfaces
{
    public interface IModelService<T> where T : BaseEntity
    {
        T GetByNumero(string numero);

        Task<T> GetByNumeroAsync(string numero);

        T[] GetAllByDate(DateTime desde, DateTime hasta);

        Task<T[]> GetAllByDateAsync(DateTime desde, DateTime hasta);

        Guid Insert(T entity);

        Task<Guid> InsertAsync(T entity);

        int Count();
    }
}
