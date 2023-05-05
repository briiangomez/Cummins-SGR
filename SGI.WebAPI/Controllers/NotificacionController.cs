using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SGIWebApi.Models;
using SGR.Models.Models;

namespace SGIWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<NotificacionesController> _logger;
        private readonly JWTSettings _jwtsettings;
        public NotificacionesController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<NotificacionesController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Notificaciones
        [HttpGet("GetNotificaciones")]
        public async Task<ActionResult<IEnumerable<Notificacione>>> GetNotificacioness()
        {
            try
            {
                var result = await _context.Notificaciones.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetNotificacionesCount")]
        public async Task<ActionResult<ItemCount>> GetNotificacionesCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Notificaciones.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Notificaciones
        [HttpGet("GetNotificacionesByPage")]
        public async Task<ActionResult<IEnumerable<Notificacione>>> GetNotificacionesByPage(int pageSize, int pageNumber)
        {

            List<Notificacione> NotificacionesList = await _context.Notificaciones.Where(o => o.Deleted == null).ToListAsync();
            NotificacionesList = NotificacionesList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(NotificacionesList);
        }

        // GET: api/Notificaciones/5
        [HttpGet("GetNotificaciones/{id}")]
        public async Task<ActionResult<Notificacione>> GetNotificaciones(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Notificaciones = await _context.Notificaciones.FindAsync(id);

                if (Notificaciones == null)
                {
                    return NotFound();
                }

                return Ok(Notificaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Notificaciones/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateNotificaciones/{id}")]
        public async Task<IActionResult> PutNotificaciones(string accessToken, Guid id, Notificacione Notificaciones)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Notificaciones.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Notificaciones).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificacionesExists(id))
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

        // POST: api/Notificaciones
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateNotificaciones")]
        public async Task<ActionResult<Notificacione>> PostNotificaciones(string accessToken, Notificacione Notificaciones)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Notificaciones.Id = Guid.NewGuid();
                Notificaciones.Created = DateTime.UtcNow;
                _context.Notificaciones.Add(Notificaciones);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetNotificaciones", new { id = Notificaciones.Id }, Notificaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Notificaciones/5
        [HttpDelete("DeleteNotificaciones/{id}")]
        public async Task<ActionResult<Notificacione>> DeleteNotificaciones(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Notificaciones = await _context.Notificaciones.FindAsync(id);
                if (Notificaciones == null)
                {
                    return NotFound();
                }
                Notificaciones.Deleted = DateTime.UtcNow;
                _context.Entry(Notificaciones).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Notificaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool NotificacionesExists(Guid id)
        {
            return _context.Notificaciones.Any(e => e.Id == id);
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