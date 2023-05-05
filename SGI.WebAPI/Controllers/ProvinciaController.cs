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
    //[Provinciaize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<ProvinciaController> _logger;
        private readonly JWTSettings _jwtsettings;
        public ProvinciaController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<ProvinciaController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Provincia
        [HttpGet("GetProvincia")]
        public async Task<ActionResult<IEnumerable<Provincium>>> GetProvincia()
        {
            try
            {
                var result = await _context.Provincia.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetProvinciaById/{id}")]
        public async Task<ActionResult<IEnumerable<Provincium>>> GetProvincias(Guid id)
        {
            try
            {
                var result = await _context.Provincia.Where(o => o.Deleted == null && o.PaisId == id).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetProvinciaCount")]
        public async Task<ActionResult<ItemCount>> GetProvinciaCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Provincia.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Provincia
        [HttpGet("GetProvinciaByPage")]
        public async Task<ActionResult<IEnumerable<Provincium>>> GetProvinciaByPage(int pageSize, int pageNumber)
        {

            List<Provincium> ProvinciaList = await _context.Provincia.Where(o => o.Deleted == null).ToListAsync();
            ProvinciaList = ProvinciaList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(ProvinciaList);
        }

        // GET: api/Provincia/5
        [HttpGet("GetProvincia/{id}")]
        public async Task<ActionResult<Provincium>> GetProvincia(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Provincia = await _context.Provincia.FindAsync(id);

                if (Provincia == null)
                {
                    return NotFound();
                }

                return Ok(Provincia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Provincia/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateProvincia/{id}")]
        public async Task<IActionResult> PutProvincia(string accessToken, Guid id, Provincium Provincia)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Provincia.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Provincia).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinciaExists(id))
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

        // POST: api/Provincia
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateProvincia")]
        public async Task<ActionResult<Provincium>> PostProvincia(string accessToken, Provincium Provincia)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Provincia.Id = Guid.NewGuid();
                Provincia.Created = DateTime.UtcNow;
                _context.Provincia.Add(Provincia);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProvincia", new { id = Provincia.Id }, Provincia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Provincia/5
        [HttpDelete("DeleteProvincia/{id}")]
        public async Task<ActionResult<Provincium>> DeleteProvincia(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Provincia = await _context.Provincia.FindAsync(id);
                if (Provincia == null)
                {
                    return NotFound();
                }
                Provincia.Deleted = DateTime.UtcNow;
                _context.Entry(Provincia).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Provincia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool ProvinciaExists(Guid id)
        {
            return _context.Provincia.Any(e => e.Id == id);
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
