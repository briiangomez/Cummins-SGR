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
    //[Certificacionize]
    [Route("api/[controller]")]
    [ApiController]
    public class CertificacionController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<CertificacionController> _logger;
        private readonly JWTSettings _jwtsettings;
        public CertificacionController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<CertificacionController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Certificacion
        [HttpGet("GetCertificacion")]
        public async Task<ActionResult<IEnumerable<Certificacion>>> GetCertificacion()
        {
            try
            {
                var result = await _context.Certificacions.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetCertificacionCount")]
        public async Task<ActionResult<ItemCount>> GetCertificacionCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Certificacions.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Certificacion
        [HttpGet("GetCertificacionByPage")]
        public async Task<ActionResult<IEnumerable<Certificacion>>> GetCertificacionByPage(int pageSize, int pageNumber)
        {

            List<Certificacion> CertificacionList = await _context.Certificacions.Where(o => o.Deleted == null).ToListAsync();
            CertificacionList = CertificacionList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(CertificacionList);
        }

        // GET: api/Certificacion/5
        [HttpGet("GetCertificacion/{id}")]
        public async Task<ActionResult<Certificacion>> GetCertificacion(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Certificacion = await _context.Certificacions.FindAsync(id);

                if (Certificacion == null)
                {
                    return NotFound();
                }

                return Ok(Certificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Certificacion/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateCertificacion/{id}")]
        public async Task<IActionResult> PutCertificacion(string accessToken, Guid id, Certificacion Certificacion)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Certificacion.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Certificacion).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificacionExists(id))
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

        // POST: api/Certificacion
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateCertificacion")]
        public async Task<ActionResult<Certificacion>> PostCertificacion(string accessToken, Certificacion Certificacion)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Certificacion.Id = Guid.NewGuid();
                Certificacion.Created = DateTime.UtcNow;
                _context.Certificacions.Add(Certificacion);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCertificacion", new { id = Certificacion.Id }, Certificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Certificacion/5
        [HttpDelete("DeleteCertificacion/{id}")]
        public async Task<ActionResult<Certificacion>> DeleteCertificacion(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Certificacion = await _context.Certificacions.FindAsync(id);
                if (Certificacion == null)
                {
                    return NotFound();
                }
                Certificacion.Deleted = DateTime.UtcNow;
                _context.Entry(Certificacion).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Certificacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool CertificacionExists(Guid id)
        {
            return _context.Certificacions.Any(e => e.Id == id);
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
