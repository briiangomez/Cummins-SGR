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
    public class IncidenciaController : Controller
    {

        private readonly IModelService<IncidenciaApi> _serviceC;
        private ILogger<IncidenciaController> _logger;
        private IMapper _mapper;
        public IncidenciaController(IMapper mapper, ILogger<IncidenciaController> logger, IModelService<IncidenciaApi> serviceC) 
        {
            _logger = logger;
            _serviceC = serviceC;
            _mapper = mapper;
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
                _logger.LogError(ex.Message, ex);
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
                _logger.LogError(ex.Message, ex);
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
                _logger.LogError(ex.Message, ex);
                result = StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            return result;
        }
 
    }
}