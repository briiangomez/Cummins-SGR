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
    //[CertificacionMotorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CertificacionMotorController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<CertificacionMotorController> _logger;
        private readonly JWTSettings _jwtsettings;
        public CertificacionMotorController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<CertificacionMotorController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/CertificacionMotor
        [HttpGet("GetCertificacionMotor")]
        public async Task<ActionResult<IEnumerable<CertificacionMotor>>> GetCertificacionMotor()
        {
            try
            {
                var result = await _context.CertificacionMotors.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetCertificacionMotorCount")]
        public async Task<ActionResult<ItemCount>> GetCertificacionMotorCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.CertificacionMotors.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/CertificacionMotor
        [HttpGet("GetCertificacionMotorByPage")]
        public async Task<ActionResult<IEnumerable<CertificacionMotor>>> GetCertificacionMotorByPage(int pageSize, int pageNumber)
        {

            List<CertificacionMotor> CertificacionMotorList = await _context.CertificacionMotors.Where(o => o.Deleted == null).ToListAsync();
            CertificacionMotorList = CertificacionMotorList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(CertificacionMotorList);
        }

        // GET: api/CertificacionMotor/5
        [HttpGet("GetCertificacionMotor/{id}")]
        public async Task<ActionResult<CertificacionMotor>> GetCertificacionMotor(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var CertificacionMotor = await _context.CertificacionMotors.FindAsync(id);

                if (CertificacionMotor == null)
                {
                    return NotFound();
                }

                return Ok(CertificacionMotor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/CertificacionMotor/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateCertificacionMotor/{id}")]
        public async Task<IActionResult> PutCertificacionMotor(string accessToken, Guid id, CertificacionMotor CertificacionMotor)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != CertificacionMotor.Id)
                {
                    return BadRequest();
                }

                _context.Entry(CertificacionMotor).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificacionMotorExists(id))
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

        // POST: api/CertificacionMotor
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateCertificacionMotor")]
        public async Task<ActionResult<CertificacionMotor>> PostCertificacionMotor(string accessToken, CertificacionMotor CertificacionMotor)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                CertificacionMotor.Id = Guid.NewGuid();
                CertificacionMotor.Created = DateTime.UtcNow;
                _context.CertificacionMotors.Add(CertificacionMotor);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCertificacionMotor", new { id = CertificacionMotor.Id }, CertificacionMotor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/CertificacionMotor/5
        [HttpDelete("DeleteCertificacionMotor/{id}")]
        public async Task<ActionResult<CertificacionMotor>> DeleteCertificacionMotor(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var CertificacionMotor = await _context.CertificacionMotors.FindAsync(id);
                if (CertificacionMotor == null)
                {
                    return NotFound();
                }
                CertificacionMotor.Deleted = DateTime.UtcNow;
                _context.Entry(CertificacionMotor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(CertificacionMotor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool CertificacionMotorExists(Guid id)
        {
            return _context.CertificacionMotors.Any(e => e.Id == id);
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
