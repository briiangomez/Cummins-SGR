using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SGI.ApplicationCore.Entities;
using SGI.ApplicationCore.Interfaces;
using SGI.ApplicationCore.Services;
using SGI.WebApp.ApiModels;
using SGI.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace SGI.WebApp.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class IncidenciaController : BaseController<Incidencia>
    {

        private readonly IncidenciaApiModelService _serviceC;
        public IncidenciaController(IServiceBase<Incidencia> service, IMapper mapper, ILogger<IncidenciaController> logger, IncidenciaApiModelService serviceC) : base(service, mapper, logger)
        {
            _serviceC = serviceC;
        }


        [HttpPost("CrearIncidencia")]
        public async Task<ActionResult<IncidenciaApi>> CrearIncidencia(IncidenciaApi entity)
        {
            ActionResult<IncidenciaApi> result;

            try
            {
                var grupoAreaNuevo = _serviceC.Insert(entity);
                result = Ok(grupoAreaNuevo);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                result = StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            return result;
        }

        [HttpGet("ObtenerTodas")]
        public async Task<ActionResult<List<IncidenciaApi>>> ObtenerTodas(DateTime desde, DateTime hasta)
        {
            ActionResult<List<IncidenciaApi>> result;
            try
            {
                var incidencias = await Task.Factory.StartNew(() => _serviceC.GetAllByDate(desde, hasta));
                result = Ok(incidencias);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                result = StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            return result;
        }


        [HttpGet("ObtenerUna/{nro}")]
        public async Task<ActionResult<IncidenciaApi>> ObtenerUna(string nro)
        {
            ActionResult<IncidenciaApi> result;
            try
            {
                var incidencias = await Task.Factory.StartNew(() => _serviceC.GetByNumero(nro));
                result = Ok(incidencias);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                result = StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            return result;
        }

        // GET api/Incidencia
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<ActionResult<IEnumerable<IncidenciaApiModel>>> Get([FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<IncidenciaApiModel>(include, filterBy, orderBy, desc);

        // GET api/Incidencia
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{page}/{pagesize}")]
        public Task<ActionResult<PagedResultViewModel<IncidenciaApiModel>>> Get(int page, int pagesize, [FromQuery] List<string> include = null, [FromQuery] string filterBy = "", [FromQuery] List<string> orderBy = null,
            [FromQuery] bool desc = false) => Query<IncidenciaApiModel>(page, pagesize, include, filterBy, orderBy, desc);

        // GET api/Incidencia/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<ActionResult<IncidenciaApiModel>> Get(Guid id, [FromQuery] List<string> include = null) => Query<IncidenciaApiModel>(id, include);

        // POST api/Incidencia
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ActionResult> Post([FromBody] IncidenciaApiModel value) => Insert(value);

        // PUT api/Incidencia/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task<ActionResult> Put(Guid id, [FromBody] IncidenciaApiModel value) => Update(id, value);


        // DELETE api/Incidencia/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(Guid id) => Drop(id);

        protected override Expression<Func<Incidencia, bool>> BuildFilterExpression(string filterBy)
         => (Incidencia x) => x.Descripcion.Contains(filterBy);

    }
}