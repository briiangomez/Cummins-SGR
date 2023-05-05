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
    //[CertificacionDealerize]
    [Route("api/[controller]")]
    [ApiController]
    public class CertificacionDealerController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<CertificacionDealerController> _logger;
        private readonly JWTSettings _jwtsettings;
        public CertificacionDealerController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<CertificacionDealerController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/CertificacionDealer
        [HttpGet("GetCertificacionDealer")]
        public async Task<ActionResult<IEnumerable<CertificacionDealer>>> GetCertificacionDealer()
        {
            try
            {
                var result = await _context.CertificacionDealers.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetCertificacionDealerCount")]
        public async Task<ActionResult<ItemCount>> GetCertificacionDealerCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.CertificacionDealers.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/CertificacionDealer
        [HttpGet("GetCertificacionDealerByPage")]
        public async Task<ActionResult<IEnumerable<CertificacionDealer>>> GetCertificacionDealerByPage(int pageSize, int pageNumber)
        {

            List<CertificacionDealer> CertificacionDealerList = await _context.CertificacionDealers.Where(o => o.Deleted == null).ToListAsync();
            CertificacionDealerList = CertificacionDealerList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(CertificacionDealerList);
        }

        // GET: api/CertificacionDealer/5
        [HttpGet("GetCertificacionDealer/{id}")]
        public async Task<ActionResult<CertificacionDealer>> GetCertificacionDealer(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var CertificacionDealer = await _context.CertificacionDealers.FindAsync(id);

                if (CertificacionDealer == null)
                {
                    return NotFound();
                }

                return Ok(CertificacionDealer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/CertificacionDealer/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateCertificacionDealer/{id}")]
        public async Task<IActionResult> PutCertificacionDealer(string accessToken, Guid id, CertificacionDealer CertificacionDealer)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != CertificacionDealer.Id)
                {
                    return BadRequest();
                }

                _context.Entry(CertificacionDealer).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificacionDealerExists(id))
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

        // POST: api/CertificacionDealer
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateCertificacionDealer")]
        public async Task<ActionResult<CertificacionDealer>> PostCertificacionDealer(string accessToken, CertificacionDealer CertificacionDealer)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                CertificacionDealer.Id = Guid.NewGuid();
                CertificacionDealer.Created = DateTime.UtcNow;
                _context.CertificacionDealers.Add(CertificacionDealer);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCertificacionDealer", new { id = CertificacionDealer.Id }, CertificacionDealer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/CertificacionDealer/5
        [HttpDelete("DeleteCertificacionDealer/{id}")]
        public async Task<ActionResult<CertificacionDealer>> DeleteCertificacionDealer(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var CertificacionDealer = await _context.CertificacionDealers.FindAsync(id);
                if (CertificacionDealer == null)
                {
                    return NotFound();
                }
                CertificacionDealer.Deleted = DateTime.UtcNow;
                _context.Entry(CertificacionDealer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(CertificacionDealer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool CertificacionDealerExists(Guid id)
        {
            return _context.CertificacionDealers.Any(e => e.Id == id);
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
