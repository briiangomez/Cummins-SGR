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
    //[EstadoIncidenciaize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoIncidenciaController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<EstadoIncidenciaController> _logger;
        private readonly JWTSettings _jwtsettings;
        public EstadoIncidenciaController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<EstadoIncidenciaController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/EstadoIncidencia
        [HttpGet("GetEstadoIncidencia")]
        public async Task<ActionResult<IEnumerable<EstadoIncidencia>>> GetEstadoIncidencias()
        {
            try
            {
                var result = await _context.EstadoIncidencias.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetEstadoIncidenciaCount")]
        public async Task<ActionResult<ItemCount>> GetEstadoIncidenciaCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.EstadoIncidencias.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/EstadoIncidencia
        [HttpGet("GetEstadoIncidenciaByPage")]
        public async Task<ActionResult<IEnumerable<EstadoIncidencia>>> GetEstadoIncidenciaByPage(int pageSize, int pageNumber)
        {

            List<EstadoIncidencia> EstadoIncidenciaList = await _context.EstadoIncidencias.Where(o => o.Deleted == null).ToListAsync();
            EstadoIncidenciaList = EstadoIncidenciaList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(EstadoIncidenciaList);
        }

        // GET: api/EstadoIncidencia/5
        [HttpGet("GetEstadoIncidencia/{id}")]
        public async Task<ActionResult<EstadoIncidencia>> GetEstadoIncidencia(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var EstadoIncidencia = await _context.EstadoIncidencias.FindAsync(id);

                if (EstadoIncidencia == null)
                {
                    return NotFound();
                }

                return Ok(EstadoIncidencia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/EstadoIncidencia/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateEstadoIncidencia/{id}")]
        public async Task<IActionResult> PutEstadoIncidencia(string accessToken, Guid id, Estado EstadoIncidencia)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != EstadoIncidencia.Id)
                {
                    return BadRequest();
                }

                _context.Entry(EstadoIncidencia).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoIncidenciaExists(id))
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

        // POST: api/EstadoIncidencia
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateEstadoIncidencia")]
        public async Task<ActionResult<EstadoIncidencia>> PostEstadoIncidencia(string accessToken, EstadoIncidencia EstadoIncidencia)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                EstadoIncidencia.Id = Guid.NewGuid();
                EstadoIncidencia.Created = DateTime.UtcNow;
                _context.EstadoIncidencias.Add(EstadoIncidencia);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEstadoIncidencia", new { id = EstadoIncidencia.Id }, EstadoIncidencia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/EstadoIncidencia/5
        [HttpDelete("DeleteEstadoIncidencia/{id}")]
        public async Task<ActionResult<EstadoIncidencia>> DeleteEstadoIncidencia(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var EstadoIncidencia = await _context.EstadoIncidencias.FindAsync(id);
                if (EstadoIncidencia == null)
                {
                    return NotFound();
                }
                EstadoIncidencia.Deleted = DateTime.UtcNow;
                _context.Entry(EstadoIncidencia).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(EstadoIncidencia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool EstadoIncidenciaExists(Guid id)
        {
            return _context.EstadoIncidencias.Any(e => e.Id == id);
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
