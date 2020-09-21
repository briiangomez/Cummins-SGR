using Microsoft.Extensions.Logging;
using SGI.ApplicationCore.DTOs;
using SGI.ApplicationCore.Entities;
using SGI.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace SGI.ApplicationCore.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : BaseEntity
    {
        private readonly ILogger<ServiceBase<T>> _logger;
        protected readonly IRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceBase(ILogger<ServiceBase<T>> logger, IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public T Get(Guid id, List<string> include = null)
        {
            return _repository.Get(id, include);
        }

        public Task<T> GetAsync(Guid id, List<string> include = null)
        {
            return _repository.GetAsync(id, include);
        }

        public virtual T[] GetAll(List<string> include = null, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null, bool desc = false)
        {
            return _repository.GetAll(include, predicate, orderBy, desc);
        }

        public virtual Task<T[]> GetAllAsync(List<string> include = null, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null, bool desc = false)
        {
            return _repository.GetAllAsync(include, predicate, orderBy, desc);
        }

        public Guid Insert(T entity)
        {
            _repository.Insert(entity);
            _unitOfWork.SaveChanges();
            return entity.Id;
        }

        public virtual async Task<Guid> InsertAsync(T entity)
        {
            await _repository.InsertAsync(entity);
            _unitOfWork.SaveChanges();
            return entity.Id;
        }

        public virtual void Update(Guid id, T entity)
        {
            T current = Get(id);

            UpdateProperties(current, entity);

            _repository.Update(current);
            _unitOfWork.SaveChanges();
        }

        protected void UpdateProperties(T current, T value)
        {
            var properties = value.GetType().GetProperties().Where(p => p != null);
            object newValue;
            PropertyInfo currentProperty;

            foreach (PropertyInfo newValueProperty in properties)
            {
                newValue = newValueProperty.GetValue(value, null);
                currentProperty = current.GetType().GetProperty(newValueProperty.Name);

                if (newValue != null &&
                    !Equals(newValue, 0) &&
                    !Equals(newValue, DateTime.MinValue))
                    currentProperty.SetValue(current, newValue);
            }
        }

        public virtual async Task UpdateAsync(Guid id, T entity)
        {
            T current = Get(id);

            UpdateProperties(current, entity);

            await _repository.UpdateAsync(current);
            _unitOfWork.SaveChanges();
        }

        public virtual void Delete(Guid id)
        {
            _repository.Delete(id);
            _unitOfWork.SaveChanges();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            _unitOfWork.SaveChanges();
        }

        public virtual PagedResult<T> GetPaged(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null)
        {
            return _repository.GetPaged(page, pagesize, include, desc, predicate, orderBy);
        }

        public virtual Task<PagedResult<T>> GetPagedAsync(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null)
        {
            return Task.Factory.StartNew(() => _repository.GetPaged(page, pagesize, include, desc, predicate, orderBy));
        }

        public int Count()
        {
            return _repository.Count();
        }
    }
}
