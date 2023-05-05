using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGR.Models.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SGIWebApi.Models;

namespace SGIWebApi.Controllers
{
    //[Paisize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<PaisController> _logger;
        private readonly JWTSettings _jwtsettings;
        public PaisController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<PaisController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Pais
        [HttpGet("GetPais")]
        public async Task<ActionResult<IEnumerable<Pai>>> GetPais()
        {
            try
            {
                var result = await _context.Pais.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetPaisCount")]
        public async Task<ActionResult<ItemCount>> GetPaisCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Pais.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Pais
        [HttpGet("GetPaisByPage")]
        public async Task<ActionResult<IEnumerable<Pai>>> GetPaisByPage(int pageSize, int pageNumber)
        {

            List<Pai> PaisList = await _context.Pais.Where(o => o.Deleted == null).ToListAsync();
            PaisList = PaisList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(PaisList);
        }

        // GET: api/Pais/5
        [HttpGet("GetPais/{id}")]
        public async Task<ActionResult<Pai>> GetPais(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Pais = await _context.Pais.FindAsync(id);

                if (Pais == null)
                {
                    return NotFound();
                }

                return Ok(Pais);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Pais/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdatePais/{id}")]
        public async Task<IActionResult> PutPais(string accessToken, Guid id, Pai Pais)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Pais.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Pais).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaisExists(id))
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

        // POST: api/Pais
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreatePais")]
        public async Task<ActionResult<Pai>> PostPais(string accessToken, Pai Pais)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Pais.Id = Guid.NewGuid();
                Pais.Created = DateTime.UtcNow;
                _context.Pais.Add(Pais);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPais", new { id = Pais.Id }, Pais);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Pais/5
        [HttpDelete("DeletePais/{id}")]
        public async Task<ActionResult<Pai>> DeletePais(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Pais = await _context.Pais.FindAsync(id);
                if (Pais == null)
                {
                    return NotFound();
                }
                Pais.Deleted = DateTime.UtcNow;
                _context.Entry(Pais).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Pais);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool PaisExists(Guid id)
        {
            return _context.Pais.Any(e => e.Id == id);
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
