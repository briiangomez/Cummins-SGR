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
    //[Estadosize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<EstadosController> _logger;
        private readonly JWTSettings _jwtsettings;
        public EstadosController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<EstadosController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Estados
        [HttpGet("GetEstados")]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstadoss()
        {
            try
            {
                var result = await _context.Estados.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetEstadosCount")]
        public async Task<ActionResult<ItemCount>> GetEstadosCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Estados.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Estados
        [HttpGet("GetEstadosByPage")]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstadosByPage(int pageSize, int pageNumber)
        {

            List<Estado> EstadosList = await _context.Estados.Where(o => o.Deleted == null).ToListAsync();
            EstadosList = EstadosList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(EstadosList);
        }

        // GET: api/Estados/5
        [HttpGet("GetEstados/{id}")]
        public async Task<ActionResult<Estado>> GetEstados(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Estados = await _context.Estados.FindAsync(id);

                if (Estados == null)
                {
                    return NotFound();
                }

                return Ok(Estados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Estados/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateEstados/{id}")]
        public async Task<IActionResult> PutEstados(string accessToken, Guid id, Estado Estados)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Estados.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Estados).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadosExists(id))
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

        // POST: api/Estados
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateEstados")]
        public async Task<ActionResult<Estado>> PostEstados(string accessToken, Estado Estados)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Estados.Id = Guid.NewGuid();
                Estados.Created = DateTime.UtcNow;
                _context.Estados.Add(Estados);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEstados", new { id = Estados.Id }, Estados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Estados/5
        [HttpDelete("DeleteEstados/{id}")]
        public async Task<ActionResult<Estado>> DeleteEstados(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Estados = await _context.Estados.FindAsync(id);
                if (Estados == null)
                {
                    return NotFound();
                }
                Estados.Deleted = DateTime.UtcNow;
                _context.Entry(Estados).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Estados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool EstadosExists(Guid id)
        {
            return _context.Estados.Any(e => e.Id == id);
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
