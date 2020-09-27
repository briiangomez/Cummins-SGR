
using Microsoft.EntityFrameworkCore;
using SGI.ApplicationCore.DTOs;
using SGI.ApplicationCore.Entities;
using SGI.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGI.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        protected SGIApplicationDataContext context;

        protected virtual DbSet<T> entities { get; set; }
        public Repository(SGIApplicationDataContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        /// <summary>
        /// Delete method
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(Guid id)
        {
            T entity = Get(id);

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.Deleted = DateTime.UtcNow;

            entities.Update(entity);
        }

        /// <summary>
        /// Method in charge of getting the entity and the related data associated with the ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public virtual T Get(Guid id, List<string> include = null)
        {
            return Get(entities, include).SingleOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Method in charge of getting all the table rows
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public virtual T[] GetAll(List<string> include = null, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null, bool desc = false)
        {
            return GetAll(entities, include, predicate, orderBy, desc).ToArray();
        }

        protected static IQueryable<T> GetAll(DbSet<T> entities, List<string> include, Expression<Func<T, bool>> predicate, List<string> orderBy, bool desc)
        {
            IQueryable<T> query = Get(entities, include, predicate);

            if (orderBy != null)
                foreach (var order in orderBy)
                    query = OrderBy(desc, query, order);

            return query;
        }

        protected static IQueryable<T> OrderBy(bool desc, IQueryable<T> query, string order)
        {
            // LAMBDA: x => x.[PropertyName]
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression property = Expression.Property(parameter, order);
            var lambda = Expression.Lambda(property, parameter);

            // REFLECTION: source.OrderBy(x => x.Property)
            var orderByMethod = typeof(Queryable).GetMethods().First(x => x.Name == (desc ? "OrderByDescending" : "OrderBy") && x.GetParameters().Length == 2);
            var orderByGeneric = orderByMethod.MakeGenericMethod(typeof(T), property.Type);
            return (IQueryable<T>)orderByGeneric.Invoke(null, new object[] { query, lambda });
        }

        protected static IQueryable<T> Get(DbSet<T> entities, List<string> include = null, Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = entities;

            if (include != null)
                foreach (string inc in include)
                    query = query.Include(inc);

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }


        /// <summary>
        /// Method in charge of getting the entity and the related data depending on a condition in a paginated presentation
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="include"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public virtual PagedResult<T> GetPaged(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null)
        {
            var data = new List<T>();
            int total = 0;
            int skip = (page - 1) * pagesize;
            IEnumerable<T> result = GetAll(entities, include, predicate, orderBy, desc).ToList();

            var pageData = result.Skip(skip).Take(pagesize).ToList();

            if (pageData != null)
            {
                total = result.Count();
                data = pageData;
            }

            return new PagedResult<T>(data, page, pagesize, total);
        }

        public virtual Task<PagedResult<T>> GetPagedAsync(int page, int pagesize, List<string> include = null, bool desc = false, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null)
        {
            return Task.Factory.StartNew(() => GetPaged(page, pagesize, include, desc, predicate, orderBy));
        }

        /// <summary>
        /// Insert method
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.Created = DateTime.UtcNow;
            entities.Add(entity);
        }

        /// <summary>
        /// Update method
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.Modified = DateTime.UtcNow;
            entities.Update(entity);
        }



        public Task<T> GetAsync(Guid id, List<string> include = null)
        {
            return Task.Factory.StartNew(() => Get(id, include));
        }

        public Task<T[]> GetAllAsync(List<string> include = null, Expression<Func<T, bool>> predicate = null, List<string> orderBy = null, bool desc = false)
        {
            return Task.Factory.StartNew(() => GetAll(include, predicate, orderBy, desc));
        }

        public Task InsertAsync(T entity)
        {
            return Task.Factory.StartNew(() => Insert(entity));
        }

        public Task UpdateAsync(T entity)
        {
            return Task.Factory.StartNew(() => Update(entity));
        }

        public Task DeleteAsync(Guid id)
        {
            return Task.Factory.StartNew(() => Delete(id));
        }

        /// <summary>
        /// Method in charge of getting the entity and the related data depending on a condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate, List<string> include = null)
        {
            IQueryable<T> query = entities;

            if (include != null)
            {
                foreach (string inc in include)
                {
                    query = query.Include(inc);
                }
            }
            query = query.Where(predicate);
            return query.OrderBy(t => t.Created).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="asNoTracking"></param>
        /// <param name="includes"></param>
        /// <param name="includes2"></param>
        /// <param name="includeDeleted"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy, bool asNoTracking = false, string[] includes2 = null, bool includeDeleted = false,
            params Expression<Func<T, object>>[] includes)
        {
            var query = asNoTracking ? entities.AsNoTracking() : entities;

            if (!includeDeleted) { query = query.Where(x => !x.Deleted.HasValue); }

            if (predicate != null) { query = query.Where(predicate); }

            if (includes != null && includes.Any())
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (includes2 != null)
            {
                foreach (var inc in includes2) { query = query.Include(inc); }
            }

            return (orderBy != null) ? orderBy(query) : query;
        }

        public int Count()
        {
            return entities.Count();
        }
    }
}
