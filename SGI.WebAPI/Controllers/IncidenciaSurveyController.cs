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
    //[IncidenciaSurveyize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncidenciaSurveyController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<IncidenciaSurveyController> _logger;
        private readonly JWTSettings _jwtsettings;
        public IncidenciaSurveyController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<IncidenciaSurveyController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/IncidenciaSurvey
        [HttpGet("GetIncidenciaSurvey")]
        public async Task<ActionResult<IEnumerable<IncidenciaSurvey>>> GetIncidenciaSurvey()
        {
            try
            {
                var result = await _context.IncidenciaSurveys.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetIncidenciaSurveyCount")]
        public async Task<ActionResult<ItemCount>> GetIncidenciaSurveyCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.IncidenciaSurveys.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/IncidenciaSurvey
        [HttpGet("GetIncidenciaSurveyByPage")]
        public async Task<ActionResult<IEnumerable<IncidenciaSurvey>>> GetIncidenciaSurveyByPage(int pageSize, int pageNumber)
        {

            List<IncidenciaSurvey> IncidenciaSurveyList = await _context.IncidenciaSurveys.Where(o => o.Deleted == null).ToListAsync();
            IncidenciaSurveyList = IncidenciaSurveyList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(IncidenciaSurveyList);
        }

        // GET: api/IncidenciaSurvey/5
        [HttpGet("GetIncidenciaSurvey/{id}")]
        public async Task<ActionResult<IncidenciaSurvey>> GetIncidenciaSurvey(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var IncidenciaSurvey = await _context.IncidenciaSurveys.FindAsync(id);

                if (IncidenciaSurvey == null)
                {
                    return NotFound();
                }

                return Ok(IncidenciaSurvey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/IncidenciaSurvey/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateIncidenciaSurvey/{id}")]
        public async Task<IActionResult> PutIncidenciaSurvey(string accessToken, Guid id, IncidenciaSurvey IncidenciaSurvey)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != IncidenciaSurvey.Id)
                {
                    return BadRequest();
                }

                _context.Entry(IncidenciaSurvey).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidenciaSurveyExists(id))
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

        // POST: api/IncidenciaSurvey
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateIncidenciaSurvey")]
        public async Task<ActionResult<IncidenciaSurvey>> PostIncidenciaSurvey(string accessToken, IncidenciaSurvey IncidenciaSurvey)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");

                var result = _context.IncidenciaSurveys.Where(o => o.IdIncidencia == IncidenciaSurvey.IdIncidencia)
                    .ToList();

                foreach (var item in result)
                {
                    _context.IncidenciaSurveys.Remove(item);
                }

                IncidenciaSurvey.Id = Guid.NewGuid();
                IncidenciaSurvey.Created = DateTime.UtcNow;
                _context.IncidenciaSurveys.Add(IncidenciaSurvey);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetIncidenciaSurvey", new { id = IncidenciaSurvey.Id }, IncidenciaSurvey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/IncidenciaSurvey/5
        [HttpDelete("DeleteIncidenciaSurvey/{id}")]
        public async Task<ActionResult<IncidenciaSurvey>> DeleteIncidenciaSurvey(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var IncidenciaSurvey = await _context.IncidenciaSurveys.FindAsync(id);
                if (IncidenciaSurvey == null)
                {
                    return NotFound();
                }
                IncidenciaSurvey.Deleted = DateTime.UtcNow;
                _context.Entry(IncidenciaSurvey).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(IncidenciaSurvey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool IncidenciaSurveyExists(Guid id)
        {
            return _context.IncidenciaSurveys.Any(e => e.Id == id);
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
