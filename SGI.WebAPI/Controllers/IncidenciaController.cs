using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGIWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace SGIWebApi.Controllers
{
    //[Incidenciaize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncidenciaController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<IncidenciaController> _logger;
        private readonly JWTSettings _jwtsettings;
        public IncidenciaController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<IncidenciaController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Incidencia
        [HttpGet("GetIncidencia")]
        public async Task<ActionResult<IEnumerable<Incidencia>>> GetIncidencias()
        {
            try
            {
                var result = await _context.Incidencias.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }


        [HttpGet("GetIncidenciaApi")]
        public async Task<ActionResult<IEnumerable<IncidenciaApi>>> GetIncidenciasApi()
        {
            try
            {
                var result = await _context.Incidencias.Where(o => o.Deleted == null).ToListAsync();
                var rest = new List<IncidenciaApi>();
                foreach (var item in result)
                {
                    rest.Add(Utility.ParseIncidencia(item, _context));
                }
                return Ok(rest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }


        [HttpGet("ObtenerTodas")]
        public async Task<ActionResult<IEnumerable<IncidenciaApi>>> ObtenerTodas(string accessToken, DateTime desde, DateTime hasta)
        {
            try
            {
                var result = await _context.Incidencias.Where(o => o.Deleted == null && o.FechaIncidencia >= desde && o.FechaIncidencia <= hasta).ToListAsync();
                var rest = new List<IncidenciaApi>();
                foreach (var item in result)
                {
                    rest.Add(Utility.ParseIncidencia(item, _context));
                }
                return Ok(rest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetIncidenciaCount")]
        public async Task<ActionResult<ItemCount>> GetIncidenciaCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Incidencias.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Incidencia
        [HttpGet("GetIncidenciaByPage")]
        public async Task<ActionResult<IEnumerable<Incidencia>>> GetIncidenciaByPage(int pageSize, int pageNumber)
        {

            List<Incidencia> IncidenciaList = await _context.Incidencias.Where(o => o.Deleted == null).ToListAsync();
            IncidenciaList = IncidenciaList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(IncidenciaList);
        }

        // GET: api/Incidencia/5
        [HttpGet("GetIncidencia/{id}")]
        public async Task<ActionResult<Incidencia>> GetIncidencia(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Incidencia = await _context.Incidencias.FindAsync(id);

                if (Incidencia == null)
                {
                    return NotFound();
                }

                return Ok(Incidencia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // GET: api/Incidencia/5
        [HttpGet("ObtenerUna/{Nro}")]
        public async Task<ActionResult<IncidenciaApi>> ObtenerUna(string accessToken, string Nro)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                long nroo = long.Parse(Nro);
                var Incidencia = await _context.Incidencias.FirstOrDefaultAsync(o => o.NroIncidenciaPauny == Nro || o.NumeroIncidencia == nroo);
                if (Incidencia == null)
                {
                    return NotFound();
                }
                var res = Utility.ParseIncidencia(Incidencia,_context);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Incidencia/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateIncidencia/{id}")]
        public async Task<IActionResult> PutIncidencia(string accessToken, Guid id, IncidenciaApi Incidencia)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Incidencia.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Incidencia).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidenciaExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // POST: api/Incidencia
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateIncidencia")]
        public async Task<ActionResult<Incidencia>> PostIncidencia(string accessToken, Incidencia Incidencia)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Incidencia.Id = Guid.NewGuid();
                Incidencia.Created = DateTime.UtcNow;
                _context.Incidencias.Add(Incidencia);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetIncidencia", new { id = Incidencia.Id }, Incidencia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }


        // POST: api/Incidencia
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CrearIncidencia")]
        public async Task<ActionResult<IncidenciaApi>> CrearIncidencia(string accessToken, IncidenciaApi entity)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Incidencia inc = new Incidencia();
                inc.Id = Guid.NewGuid();
                
                inc.Created = DateTime.Now;
                inc.Modified = DateTime.Now;
                inc.NumeroIncidencia = _context.Incidencias.Count() + 1;
                inc.NroIncidenciaPauny = entity.numeroIncidencia.ToString();
                Motor mot = new Motor();
                Cliente cli = new Cliente();
                Falla fal = new Falla();
                EstadoGarantia estG = new EstadoGarantia();
                Dealer dea = _context.Dealers.FirstOrDefault(o => o.Name.Contains(entity.nombreConcesionario) && o.LocationCode.Contains(entity.codigoConcesionario));
                if (dea == null)
                {
                    dea = new Dealer();
                    dea.Id = Guid.NewGuid();
                    dea.Created = DateTime.Now;
                    dea.Name = entity.nombreConcesionario;
                    dea.Phone = entity.telefonoConcesionario;
                    dea.Email = entity.emailConcesionario;
                    dea.DistributorCode = entity.codigoConcesionario;
                    dea.LatitudGps = entity.latitudGps;
                    dea.LongitudGps = entity.longitudGps;
                    await _context.Dealers.AddAsync(dea);
                    await _context.SaveChangesAsync();
                }
                inc.IdDealer = dea.Id;
                cli = _context.Clientes.FirstOrDefault(o => o.Nombre.Contains(entity.nombreContacto) && o.Dni.Contains(entity.numeroDocumento));
                if(cli == null)
                {
                    cli = new Cliente();
                    cli.Id = Guid.NewGuid();
                    cli.Created = DateTime.Now;
                    cli.TipoDni = entity.tipoDniContacto;
                    cli.Dni = entity.numeroDocumento;
                    cli.Nombre = entity.nombreContacto;
                    cli.Direccion = entity.domicilioContacto;
                    cli.Localidad = entity.localidadContacto;
                    cli.Provincia = entity.provinciaContacto;
                    cli.Telefono = entity.telefonoFijoContacto;
                    cli.Celular = entity.telefonoCelularContacto;
                    cli.Email = entity.emailContacto;
                    cli.LatitudGpsContacto = entity.latitudGpsContacto;
                    cli.LongitudGpsContacto = entity.longitudGpsContacto;
                    await _context.Clientes.AddAsync(cli);
                    await _context.SaveChangesAsync();
                }
                inc.IdCliente = cli.Id;
                Estado est = _context.Estados.FirstOrDefault(o => o.Descripcion.Contains(entity.nombreEstadoIncidencia));
                if(est == null)
                {
                    est = _context.Estados.FirstOrDefault(o => o.Descripcion.Contains("Pendiente"));
                }
                inc.Id = Guid.NewGuid();
                inc.NumeroIncidencia = entity.numeroIncidencia;
                inc.NumeroOperacion = entity.numeroOperacion;
                if (entity.fechaIncidencia != null)
                {
                    inc.FechaIncidencia = entity.fechaIncidencia.Value;
                }
                if (entity.fechaRegistro != null)
                {
                    inc.FechaRegistro = entity.fechaRegistro.Value;
                }
                if (entity.fechaCierre != null)
                {
                    inc.FechaCierre = entity.fechaCierre.Value;
                }
                inc.NroReclamoConcesionario = entity.NroReclamoConcesionario;
                inc.NroReclamoCummins = entity.NroReclamoCummins;
                inc.Descripcion = entity.Descripcion;
                inc.LatitudGps = entity.latitudGps;
                inc.LongitudGps = entity.latitudGps;
                inc.PathImagenes = entity.PathImagenes;
                inc.EsGarantia = entity.EsGarantia;
                inc.MostrarEnTv = entity.mostrarEnTv;
                inc.Sintoma = entity.Sintoma;
                await _context.Incidencias.AddAsync(inc);
                await _context.SaveChangesAsync();
                mot = _context.Motors.Where(o => o.NumeroMotor == entity.numeroMotor && o.Equipo == entity.Equipo).FirstOrDefault();
                if (mot == null)
                {
                    mot = new Motor();
                    mot.Created = DateTime.Now;
                    mot.Equipo = entity.Equipo;
                    mot.NumeroMotor = entity.numeroMotor;
                    mot.Id = Guid.NewGuid();
                    await _context.Motors.AddAsync(mot);
                    await _context.SaveChangesAsync();
                }
                MotorIncidencia mots = new MotorIncidencia();
                mots.Created = DateTime.Now;
                mots.Modified = DateTime.Now;
                mots.NumeroChasis = entity.numeroChasis;
                mots.ModeloEquipo = entity.ModeloEquipo;
                mots.HsKm = entity.horasTractor;
                mots.IncidenciaId = inc.Id;
                mots.MotorId = mot.Id;
                if (entity.fechaCompra != null)
                {
                    mots.FechaCompra = entity.fechaCompra.Value;
                }
                if (entity.fechaFalla != null)
                {
                    mots.FechaFalla = entity.fechaFalla.Value;
                }
                if (entity.fechaInicioGarantia != null)
                {
                    mots.FechaInicioGarantia = entity.fechaInicioGarantia.Value;
                }
                EstadoIncidencia estt = new EstadoIncidencia();
                estt.Created = DateTime.Now;
                estt.EstadoId = est.Id;
                estt.IncidenciaId = inc.Id;
                await _context.EstadoIncidencias.AddAsync(estt);
                await _context.SaveChangesAsync();
                fal.IdFalla = (int)entity.idFalla;
                fal.Created = DateTime.Now;
                fal.Observaciones = entity.observacionesFalla;
                fal.Nombre = entity.nombreFalla;
                fal.Codigo = entity.codigoSintoma.ToString();
                fal.IdIncidencia = inc.Id;
                await _context.Fallas.AddAsync(fal);
                await _context.SaveChangesAsync();
                await _context.MotorIncidencias.AddAsync(mots);
                await _context.SaveChangesAsync();
                estG.Created = DateTime.Now;
                estG.Nombre = entity.nombreEstadoGarantia;
                estG.ObservacionesGarantia = entity.observacionesGarantia;
                estG.ObservacionesProveedor = entity.observacionesProveedor;
                estG.IdIncidencia = inc.Id;
                await _context.EstadoGarantias.AddAsync(estG);
                await _context.SaveChangesAsync();
                entity.Id = inc.Id;
                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Incidencia/5
        [HttpDelete("DeleteIncidencia/{id}")]
        public async Task<ActionResult<Incidencia>> DeleteIncidencia(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Incidencia = await _context.Incidencias.FindAsync(id);
                if (Incidencia == null)
                {
                    return NotFound();
                }
                Incidencia.Deleted = DateTime.UtcNow;
                _context.Entry(Incidencia).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Incidencia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool IncidenciaExists(Guid id)
        {
            return _context.Incidencias.Any(e => e.Id == id);
        }

        private bool GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principle.FindFirst(ClaimTypes.Name)?.Value;
                    var id = Guid.Parse(userId);

                    var user = _context.Users.FindAsync(id);

                    if (user != null)
                        return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }


       
    }
}
