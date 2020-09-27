using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGI.ApplicationCore.Entities;
using SGI.ApplicationCore.Interfaces;
using SGI.WebApp.ApiModels;
using SGI.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGI.WebApp.Controllers
{
    public abstract class BaseController<T> : ControllerBase where T : BaseEntity
    {
        public ILogger<BaseController<T>> Logger { get; }
        public IServiceBase<T> Service { get; }

        public IMapper Mapper { get; }

        protected virtual Expression<Func<T, bool>> BuildFilterExpression(string filterBy) => throw new NotImplementedException();

        /// <summary>
        /// 
        /// <param name="service"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        protected BaseController(IServiceBase<T> service, IMapper mapper, ILogger<BaseController<T>> logger)
        {
            Service = service;
            Logger = logger;
            Mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <returns></returns>
        protected async Task<ActionResult<IEnumerable<V>>> Query<V>(List<string> include = null, string filterBy = "", List<string> orderBy = null, bool desc = false)
        {
            try
            {
                var current = await Service.GetAllAsync(include, GetPredicate(filterBy), orderBy, desc);
                var result = await Task.Factory.StartNew(() => Mapper.Map<IEnumerable<V>>(current));

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="include"></param>
        /// <param name="desc"></param>
        /// <param name="filterBy"></param>
        /// <returns></returns>
        protected async Task<ActionResult<PagedResultViewModel<V>>> Query<V>(int page, int pagesize, List<string> include = null, string filterBy = "", List<string> orderBy = null, bool desc = false)
            where V : BaseApiModel
        {
            try
            {
                var current = await Service.GetPagedAsync(page, pagesize, include, desc, GetPredicate(filterBy), orderBy);
                var result = await Task.Factory.StartNew(() => Mapper.Map<PagedResultViewModel<V>>(current));

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        /// <summary>
        ///     Gets an entity by id and return the ViewModel specified by parameter.       
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="id"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        protected async Task<ActionResult<V>> Query<V>(Guid id, List<string> include = null)
            where V : BaseApiModel
        {
            try
            {
                var current = await Service.GetAsync(id, include);
                var result = await Task.Factory.StartNew(() => Mapper.Map<V>(current));

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }


        /// <summary>
        ///    Save the Entity based on the ViewModel
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected async Task<ActionResult> Insert<V>([FromBody] V value)
            where V : BaseApiModel
        {
            try
            {
                T current = await Task.Factory.StartNew(() => Mapper.Map<T>(value));
                Guid result = await Service.InsertAsync(current);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        /// <summary>
        ///     Metodo encargado de actualizar la entidad a partir
        ///     del viewModel asocado a la entidad
        /// </summary>
        /// <param name="id"></param>
        /// <param name="valueViewModel"></param>
        /// <returns></returns>
        protected async Task<ActionResult> Update<V>(Guid id, [FromBody] V valueViewModel)
            where V : BaseApiModel
        {
            try
            {
                var value = await Task.Factory.StartNew(() => Mapper.Map<T>(valueViewModel));

                await Service.UpdateAsync(id, value);
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }



        /// <summary>
        ///     Metodo encargado de realizar una baja logica a partir
        ///     de un identificador asociado al elemento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async Task<ActionResult> Drop(Guid id)
        {
            try
            {
                await Service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        /// <summary>
        ///     Performs a filter over the entity.
        /// </summary>
        /// <param name="filterBy"></param>
        /// <returns></returns>
        virtual protected Expression<Func<T, bool>> GetPredicate(string filterBy)
        {
            return !string.IsNullOrEmpty(filterBy) ? BuildFilterExpression(filterBy) : default(Expression<Func<T, bool>>);
        }

        /// <summary>
        /// Returns the Count value of the current entity.
        /// </summary>
        /// <returns>int</returns>
        protected int Count()
        {
            return Service.Count();
        }
    }
}
