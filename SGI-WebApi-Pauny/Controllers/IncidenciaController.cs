using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SGI_WebApi_Pauny.Models;

namespace SGI_WebApi_Pauny.Controllers
{
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

        [HttpGet("ObtenerTodas/{desde}/{hasta}")]
        public async Task<ActionResult<IEnumerable<IncidenciaApi>>> ObtenerTodas(string accessToken, DateTime desde, DateTime hasta)
        {
            try
            {
                if (!GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var result = await _context.Incidencias.Where(o => o.Deleted == null && o.FechaIncidencia >= desde && o.FechaIncidencia <= hasta).ToListAsync();
                var rest = new List<IncidenciaApi>();
                foreach (var item in result)
                {
                    rest.Add(ParseIncidencia(item));
                }
                return Ok(rest);
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
                if (!GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                long nroo = long.Parse(Nro);
                var Incidencia = await _context.Incidencias.FirstOrDefaultAsync(o => o.NroIncidenciaPauny == Nro || o.NumeroIncidencia == nroo);
                if (Incidencia == null)
                {
                    return NotFound();
                }
                var res = ParseIncidencia(Incidencia);
                return Ok(res);
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
        public async Task<ActionResult<IncidenciaApi>> 
            CrearIncidencia(IncidenciaApi entity)
        {
            try
            {
                if (!GetUserFromAccessToken(entity.accessToken))
                    throw new Exception("Invalid Token");
                string pauny = entity.numeroIncidencia.ToString();
                if (!_context.Incidencias.Where(o => o.NroIncidenciaPauny == pauny).Any())
                {
                    Incidencia inc = new Incidencia();
                    inc.Id = Guid.NewGuid();
                    inc.Created = DateTime.Now;
                    inc.Modified = DateTime.Now;
                    inc.NumeroIncidencia = _context.Incidencias.Count() + 1;
                    inc.NroIncidenciaPauny = entity.numeroIncidencia.ToString();
                    inc.ConfiguracionCorta = entity.configuracionCorta;
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
                        dea.LocationCode = entity.codigoConcesionario;
                        dea.LatitudGps = entity.latitudGps;
                        dea.LongitudGps = entity.longitudGps;
                        await _context.Dealers.AddAsync(dea);
                        await _context.SaveChangesAsync();
                    }
                    inc.IdDealer = dea.Id;
                    cli = _context.Clientes.FirstOrDefault(o => o.Nombre.Contains(entity.nombreContacto) && o.Dni.Contains(entity.numeroDocumento));
                    if (cli == null)
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
                    inc.Aux2 = entity.nombreEstadoIncidencia;
                    inc.Aux3 = entity.idEstadoIncidencia.ToString();
                    Estado est = new Estado();
                    if (entity.idEstadoIncidencia == 0)
                    {
                        est = _context.Estados.FirstOrDefault(o => o.Descripcion.Contains("Pendiente"));
                    }
                    else if (entity.idEstadoIncidencia == 1 || entity.idEstadoIncidencia == 2)
                    {
                        est = _context.Estados.FirstOrDefault(o => o.Descripcion.Contains("En Proceso"));
                    }
                    //else if (entity.idEstadoIncidencia == 3)
                    //{
                    //    est = _context.Estados.FirstOrDefault(o => o.Descripcion.Contains("Cerrado"));
                    //}
                    else if (entity.idEstadoIncidencia == 4)
                    {
                        est = _context.Estados.FirstOrDefault(o => o.Descripcion.Contains("Anulado"));
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
                    //if (entity.fechaCierre != null)
                    //{
                    //    inc.FechaCierre = entity.fechaCierre.Value;
                    //}
                    //inc.NroReclamoConcesionario = entity.NroReclamoConcesionario;
                    //inc.NroReclamoCummins = entity.NroReclamoCummins;
                    //inc.Descripcion = entity.Descripcion;
                    string codSin = entity.codigoSintoma.ToString();
                    Sintoma sint = _context.Sintomas.FirstOrDefault(o => o.Codigo == codSin);
                    if(sint != null)
                    {
                        inc.Sintoma = sint.Descripcion;
                    }
                    else
                    {
                        inc.Sintoma = entity.Sintoma;
                    }
                    if (entity.fechaPreEntrega != null)
                    {
                        inc.FechaPreEntrega = entity.fechaPreEntrega.Value;
                    }
                    inc.LatitudGps = entity.latitudGps;
                    inc.LongitudGps = entity.latitudGps;
                    inc.PathImagenes = entity.PathImagenes;
                    inc.EsGarantia = entity.EsGarantia;
                    inc.MostrarEnTv = entity.mostrarEnTv;
                    inc.Sintoma = entity.Sintoma;
                    inc.Aux1 = entity.ObservacionesIncidencia;
                    await _context.Incidencias.AddAsync(inc);
                    await _context.SaveChangesAsync();
                    mot = _context.Motors.Where(o => o.NumeroMotor == entity.numeroMotor && o.Equipo == entity.Equipo).FirstOrDefault();
                    if (mot == null)
                    {
                        mot = new Motor();
                        mot.Created = DateTime.Now;
                        mot.Equipo = entity.Equipo;
                        mot.Modelo = entity.ModeloMotor;
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
                    if(entity.idEstadoGarantia == 4)
                    {
                        estt.Observacion = entity.observacionesAnulada;
                    }
                    await _context.EstadoIncidencias.AddAsync(estt);
                    await _context.SaveChangesAsync();
                    fal.IdFalla = (int)entity.idFalla;
                    fal.Created = DateTime.Now;
                    fal.Observaciones = entity.observacionesFalla;
                    fal.Nombre = entity.nombreFalla;
                    fal.Codigo = entity.idFalla.ToString();
                    fal.IdIncidencia = inc.Id;
                    await _context.Fallas.AddAsync(fal);
                    await _context.SaveChangesAsync();
                    await _context.MotorIncidencias.AddAsync(mots);
                    await _context.SaveChangesAsync();
                    estG.IdEstadoGarantia = entity.idEstadoGarantia;
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
                else
                {
                    Incidencia inc = _context.Incidencias.FirstOrDefault(o => o.NroIncidenciaPauny == pauny);
                    inc.Modified = DateTime.Now;
                    inc.ConfiguracionCorta = entity.configuracionCorta;
                    Motor mot = new Motor();
                    Cliente cli = new Cliente();
                    
                    Dealer dea = _context.Dealers.FirstOrDefault(o => o.Name.Contains(entity.nombreConcesionario) && o.LocationCode.Contains(entity.codigoConcesionario));
                    if (dea == null)
                    {
                        dea = new Dealer();
                        dea.Id = Guid.NewGuid();
                        dea.Created = DateTime.Now;
                        dea.Name = entity.nombreConcesionario;
                        dea.Phone = entity.telefonoConcesionario;
                        dea.Email = entity.emailConcesionario;
                        dea.LocationCode = entity.codigoConcesionario;
                        dea.LatitudGps = entity.latitudGps;
                        dea.LongitudGps = entity.longitudGps;
                        await _context.Dealers.AddAsync(dea);
                        await _context.SaveChangesAsync();
                    }
                    inc.IdDealer = dea.Id;
                    cli = _context.Clientes.FirstOrDefault(o => o.Nombre.Contains(entity.nombreContacto) && o.Dni.Contains(entity.numeroDocumento));
                    if (cli == null)
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
                    inc.Aux2 = entity.nombreEstadoIncidencia;
                    inc.Aux3 = entity.idEstadoIncidencia.ToString();
                    Estado est = new Estado();
                    EstadoIncidencia estt = _context.EstadoIncidencias.OrderByDescending(o => o.Created).FirstOrDefault(o => o.IncidenciaId == inc.Id);
                    if (entity.idEstadoIncidencia == 0)
                    {
                        est = _context.Estados.FirstOrDefault(o => o.Descripcion.Contains("Pendiente"));
                    }
                    else if (entity.idEstadoIncidencia == 1 || entity.idEstadoIncidencia == 2)
                    {
                        est = _context.Estados.FirstOrDefault(o => o.Descripcion.Contains("En Proceso"));
                    }
                    else if (entity.idEstadoIncidencia == 4)
                    {
                        est = _context.Estados.FirstOrDefault(o => o.Descripcion.Contains("Anulado"));
                    }
                    else
                    {
                        est = _context.Estados.Find(estt.EstadoId);
                    }
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
                    inc.LatitudGps = entity.latitudGps;
                    inc.LongitudGps = entity.latitudGps;
                    inc.PathImagenes = entity.PathImagenes;
                    inc.EsGarantia = entity.EsGarantia;
                    inc.MostrarEnTv = entity.mostrarEnTv;
                    inc.Sintoma = entity.Sintoma;
                    inc.Aux1 = entity.ObservacionesIncidencia;
                    string codSin = entity.codigoSintoma.ToString();
                    Sintoma sint = _context.Sintomas.FirstOrDefault(o => o.Codigo == codSin);
                    if (sint != null)
                    {
                        inc.Sintoma = sint.Descripcion;
                    }
                    else
                    {
                        inc.Sintoma = entity.Sintoma;
                    }
                    if (entity.fechaPreEntrega != null)
                    {
                        inc.FechaPreEntrega = entity.fechaPreEntrega.Value;
                    }
                    _context.Entry(inc).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    mot = _context.Motors.Where(o => o.NumeroMotor == entity.numeroMotor && o.Equipo == entity.Equipo).FirstOrDefault();
                    if (mot == null)
                    {
                        mot = new Motor();
                        mot.Created = DateTime.Now;
                        mot.Equipo = entity.Equipo;
                        mot.NumeroMotor = entity.numeroMotor;
                        mot.Modelo = entity.ModeloMotor;
                        mot.Id = Guid.NewGuid();
                        await _context.Motors.AddAsync(mot);
                        await _context.SaveChangesAsync();
                    }
                    
                    MotorIncidencia mots = _context.MotorIncidencias.FirstOrDefault(o => o.IncidenciaId == inc.Id);
                    if(mots == null)
                    {
                        mots = new MotorIncidencia();
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
                        await _context.MotorIncidencias.AddAsync(mots);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
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
                        _context.Entry(mots).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    
                    if (estt == null)
                    {
                        estt = new EstadoIncidencia();
                        estt.Created = DateTime.Now;
                        estt.EstadoId = est.Id;
                        estt.IncidenciaId = inc.Id;
                        if (entity.idEstadoGarantia == 4)
                        {
                            estt.Observacion = entity.observacionesAnulada;
                        }
                        await _context.EstadoIncidencias.AddAsync(estt);
                        await _context.SaveChangesAsync(); 
                    }
                    else
                    {
                        estt.Created = DateTime.Now;
                        estt.EstadoId = est.Id;
                        estt.IncidenciaId = inc.Id;
                        if (entity.idEstadoGarantia == 4)
                        {
                            estt.Observacion = entity.observacionesAnulada;
                        }
                        _context.Entry(estt).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }

                    Falla fal = _context.Fallas.FirstOrDefault(o => o.IdIncidencia == inc.Id);
                    if (fal == null)
                    {
                        fal = new Falla();
                        fal.IdFalla = (int)entity.idFalla;
                        fal.Created = DateTime.Now;
                        fal.Observaciones = entity.observacionesFalla;
                        fal.Nombre = entity.nombreFalla;
                        fal.Codigo = entity.idFalla.ToString();
                        fal.IdIncidencia = inc.Id;
                        await _context.Fallas.AddAsync(fal);
                        await _context.SaveChangesAsync(); 
                    }
                    else
                    {
                        fal.IdFalla = (int)entity.idFalla;
                        fal.Observaciones = entity.observacionesFalla;
                        fal.Nombre = entity.nombreFalla;
                        fal.Codigo = entity.idFalla.ToString();
                        fal.IdIncidencia = inc.Id;
                        _context.Entry(fal).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }

                    EstadoGarantia estG = _context.EstadoGarantias.FirstOrDefault(o => o.IdIncidencia == inc.Id);
                    if (estG == null)
                    {
                        estG = new EstadoGarantia();
                        estG.Created = DateTime.Now;
                        estG.IdEstadoGarantia = entity.idEstadoGarantia;
                        estG.Nombre = entity.nombreEstadoGarantia;
                        estG.ObservacionesGarantia = entity.observacionesGarantia;
                        estG.ObservacionesProveedor = entity.observacionesProveedor;
                        estG.IdIncidencia = inc.Id;
                        await _context.EstadoGarantias.AddAsync(estG);
                        await _context.SaveChangesAsync(); 
                    }
                    else
                    {
                        estG.IdEstadoGarantia = entity.idEstadoGarantia;
                        estG.Nombre = entity.nombreEstadoGarantia;
                        estG.ObservacionesGarantia = entity.observacionesGarantia;
                        estG.ObservacionesProveedor = entity.observacionesProveedor;
                        estG.IdIncidencia = inc.Id;
                        _context.Entry(estG).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    entity.Id = inc.Id;
                    return Ok(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }


        private bool GetUserFromAccessToken(string accessToken)
        {
            try
            {
                if(!String.IsNullOrEmpty(accessToken))
                {
                    var requestToken = _context.RefreshTokens.FirstOrDefault(o => o.Token == accessToken);

                    var user = _context.Users.Find(requestToken.IdUser);

                    if (user.EmailAddress != "pauny")
                        return true;
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

        // POST: api/Users
        [HttpPost("Login/{username}/{password}")]
        public async Task<ActionResult<UserWithToken>> Login(string username, string password)
        {
            try
            {
                password = Utility.Encrypt(password);

                User user = await _context.Users.Include(u => u.IdRoleNavigation)
                            .Where(u => u.EmailAddress == username
                                    && u.Password == password).FirstOrDefaultAsync();

                UserWithToken userWithToken = null;
                RefreshToken refreshToken = new RefreshToken();

                if (user == null)
                {
                    return NotFound();
                }
                if (user != null)
                {
                    refreshToken = GenerateRefreshToken();
                    user.RefreshTokens.Add(refreshToken);
                    await _context.SaveChangesAsync();

                    userWithToken = new UserWithToken(user);
                    userWithToken.RefreshToken = refreshToken.Token;
                }

                if (userWithToken == null)
                {
                    return NotFound();
                }

                //sign your token here here..
                userWithToken.AccessToken = GenerateAccessToken(user.Id);
                return Ok(refreshToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);
            refreshToken.Created = DateTime.UtcNow;
            return refreshToken;
        }


        private string GenerateAccessToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [NonAction]
        protected IncidenciaApi ParseIncidencia(Incidencia inc)
        {
            IncidenciaApi incidencia = new IncidenciaApi();
            try
            {
                var motInc = _context.MotorIncidencias.FirstOrDefault(o => o.IncidenciaId == inc.Id);
                var mot = _context.Motors.FirstOrDefault(o => o.Id == motInc.MotorId);
                var falla = _context.Fallas.FirstOrDefault(o => o.IdIncidencia == inc.Id);
                var cliente = _context.Clientes.FirstOrDefault(o => o.Id == inc.IdCliente);
                var estadoG = _context.EstadoGarantias.FirstOrDefault(o => o.IdIncidencia == inc.Id);
                var estInc = _context.EstadoIncidencias.OrderByDescending(o => o.Created).FirstOrDefault(o => o.IncidenciaId == inc.Id);
                var estado = _context.Estados.FirstOrDefault(o => o.Id == estInc.EstadoId);
                var dealer = _context.Dealers.FirstOrDefault(o => o.Id == inc.IdDealer);
                var sintomas = _context.Sintomas.ToList();
                incidencia.Id = inc.Id;
                incidencia.numeroDocumento = cliente.Dni;
                incidencia.fechaIncidencia = inc.FechaIncidencia;
                incidencia.fechaRegistro = inc.FechaRegistro;
                incidencia.fechaCompra = motInc.FechaCompra;
                //if(inc.FechaCierre != null)
                //{
                //    incidencia.fe = inc.FechaCierre.Value;
                //}
                if (!String.IsNullOrEmpty(inc.Sintoma))
                {
                    incidencia.Sintoma = inc.Sintoma;
                    incidencia.codigoSintoma = 0;
                    if (sintomas.FirstOrDefault(o => o.Descripcion == inc.Sintoma) != null)
                    {
                        incidencia.codigoSintoma = Int32.Parse(sintomas.FirstOrDefault(o => o.Descripcion == inc.Sintoma).Codigo);
                    }
                }
                //if (inc.NroReclamoConcesionario != null)
                //{
                //    incidencia.NroReclamoConcesionario = inc.NroReclamoConcesionario.Value;
                //}
                //if (inc.NroReclamoCummins != null)
                //{
                //    incidencia.NroReclamoCummins = inc.NroReclamoCummins.Value;
                //}
                //incidencia.Descripcion = inc.Descripcion;
                //incidencia.DireccionInspeccion = inc.DireccionInspeccion;
                incidencia.latitudGps = (long)inc.LatitudGps;
                incidencia.longitudGps = (long)inc.LongitudGps;
                incidencia.PathImagenes = inc.PathImagenes;
                if (inc.MostrarEnTv != null)
                {
                    incidencia.mostrarEnTv = inc.MostrarEnTv.Value;
                }
                if (estadoG.IdEstadoGarantia != null)
                {
                    incidencia.idEstadoGarantia = estadoG.IdEstadoGarantia.Value;
                }
                if (inc.EsGarantia != null)
                {
                    incidencia.EsGarantia = inc.EsGarantia.Value;
                }
                incidencia.numeroOperacion = inc.NumeroOperacion;
                incidencia.numeroIncidencia = inc.NumeroIncidencia;
                incidencia.numeroChasis = motInc.NumeroChasis;
                incidencia.numeroMotor = mot.NumeroMotor;
                incidencia.Equipo = mot.Equipo;
                incidencia.ModeloEquipo = motInc.ModeloEquipo;
                incidencia.configuracionCorta = inc.ConfiguracionCorta;
                incidencia.fechaPreEntrega = inc.FechaPreEntrega;
                if (motInc.HsKm != null)
                {
                    incidencia.horasTractor = motInc.HsKm.Value;
                }
                if (motInc.FechaInicioGarantia != null)
                {
                    incidencia.fechaInicioGarantia = motInc.FechaInicioGarantia.Value;
                }
                if (motInc.FechaFalla != null)
                {
                    incidencia.fechaFalla = motInc.FechaFalla.Value;
                }
                incidencia.ModeloMotor = mot.Modelo;
                incidencia.telefonoCelularContacto = cliente.Celular;
                incidencia.ObservacionesIncidencia = inc.Aux1;
                incidencia.codigoConcesionario = dealer.LocationCode;
                incidencia.nombreConcesionario = dealer.Name;
                incidencia.emailConcesionario = dealer.Email;
                incidencia.telefonoConcesionario = dealer.Phone;
                incidencia.nombreContacto = cliente.Nombre;
                incidencia.tipoDniContacto = cliente.TipoDni;
                incidencia.numeroDocumento = cliente.Dni;
                incidencia.domicilioContacto = cliente.Direccion;
                incidencia.localidadContacto = cliente.Localidad;
                incidencia.provinciaContacto = cliente.Provincia;
                incidencia.telefonoFijoContacto = cliente.Telefono;
                incidencia.emailContacto = cliente.Email;
                incidencia.latitudGpsContacto = (float)cliente.LatitudGpsContacto;
                incidencia.longitudGpsContacto = (float)cliente.LongitudGpsContacto;
                incidencia.idFalla = (long)falla.IdFalla;
                incidencia.nombreFalla = falla.Nombre;
                incidencia.observacionesFalla = falla.Observaciones;
                incidencia.nombreEstadoIncidenciaSGI = estado.Descripcion;
                if (estado.Descripcion == "Anulada")
                {
                    incidencia.observacionesAnulada = estInc.Observacion;
                }
                incidencia.idEstadoIncidenciaSGI = estado.Codigo;
                incidencia.nombreEstadoIncidencia = inc.Aux2;
                if (!String.IsNullOrEmpty(inc.Aux3))
                {
                    incidencia.idEstadoIncidencia = Int32.Parse(inc.Aux3);
                }
                incidencia.fechaEstadoIncidencia = estInc.Created;
                if (estadoG.IdEstadoGarantia != null)
                {
                    incidencia.idEstadoGarantia = estadoG.IdEstadoGarantia.Value;
                }
                incidencia.nombreEstadoGarantia = estadoG.Nombre;
                incidencia.observacionesGarantia = estadoG.ObservacionesGarantia;
                incidencia.observacionesProveedor = estadoG.ObservacionesProveedor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
            return incidencia;

        }
    }
}