namespace CMM.Data
{
    using System;
    using System.Linq;

    public interface IRepository<T>
    {
        bool Add(T item);
        IQueryable<T> AsQueryable();
        bool Remove(T item);
        bool Update(T item);
    }
}

