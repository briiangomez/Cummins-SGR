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
    //[Sintomaize]
    [Route("api/[controller]")]
    [ApiController]
    public class SintomaController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<SintomaController> _logger;
        private readonly JWTSettings _jwtsettings;
        public SintomaController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<SintomaController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Sintoma
        [HttpGet("GetSintoma")]
        public async Task<ActionResult<IEnumerable<Sintoma>>> GetSintoma()
        {
            try
            {
                //if (GetUserFromAccessToken(accessToken))
                    //throw new Exception("Invalid Token");
                var result = await _context.Sintomas.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetSintomaCount")]
        public async Task<ActionResult<ItemCount>> GetSintomaCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Sintomas.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Sintoma
        [HttpGet("GetSintomaByPage")]
        public async Task<ActionResult<IEnumerable<Sintoma>>> GetSintomaByPage(int pageSize, int pageNumber)
        {

            List<Sintoma> SintomaList = await _context.Sintomas.Where(o => o.Deleted == null).ToListAsync();
            SintomaList = SintomaList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(SintomaList);
        }

        // GET: api/Sintoma/5
        [HttpGet("GetSintoma/{id}")]
        public async Task<ActionResult<Sintoma>> GetSintoma(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Sintoma = await _context.Sintomas.FindAsync(id);

                if (Sintoma == null)
                {
                    return NotFound();
                }

                return Ok(Sintoma);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Sintoma/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateSintoma/{id}")]
        public async Task<IActionResult> PutSintoma(string accessToken, Guid id, Sintoma Sintoma)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Sintoma.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Sintoma).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SintomaExists(id))
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

        // POST: api/Sintoma
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateSintoma")]
        public async Task<ActionResult<Sintoma>> PostSintoma(string accessToken, Sintoma Sintoma)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Sintoma.Id = Guid.NewGuid();
                Sintoma.Created = DateTime.UtcNow;
                _context.Sintomas.Add(Sintoma);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSintoma", new { id = Sintoma.Id }, Sintoma);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Sintoma/5
        [HttpDelete("DeleteSintoma/{id}")]
        public async Task<ActionResult<Sintoma>> DeleteSintoma(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Sintoma = await _context.Sintomas.FindAsync(id);
                if (Sintoma == null)
                {
                    return NotFound();
                }
                Sintoma.Deleted = DateTime.UtcNow;
                _context.Entry(Sintoma).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Sintoma);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool SintomaExists(Guid id)
        {
            return _context.Sintomas.Any(e => e.Id == id);
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
