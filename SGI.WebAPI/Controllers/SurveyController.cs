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
    //[Surveyize]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<SurveyController> _logger;
        private readonly JWTSettings _jwtsettings;
        public SurveyController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<SurveyController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Survey
        [HttpGet("GetSurvey")]
        public async Task<ActionResult<IEnumerable<Survey>>> GetSurveys()
        {
            try
            {
                var result = await _context.Surveys.Where(o => o.Deleted == null).Include(x => x.SurveyItems)
                .ThenInclude(x => x.SurveyItemOptions).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetSurveyCount")]
        public async Task<ActionResult<ItemCount>> GetSurveyCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Surveys.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Survey
        [HttpGet("GetSurveyByPage")]
        public async Task<ActionResult<IEnumerable<Survey>>> GetSurveyByPage(int pageSize, int pageNumber)
        {

            List<Survey> SurveyList = await _context.Surveys.Where(o => o.Deleted == null).ToListAsync();
            SurveyList = SurveyList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(SurveyList);
        }

        // GET: api/Survey/5
        [HttpGet("GetSurvey/{id}")]
        public async Task<ActionResult<Survey>> GetSurvey(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var result = await _context.Surveys.Where(o => o.Deleted == null).Include(x => x.SurveyItems)
                .ThenInclude(x => x.SurveyItemOptions).ToListAsync();
                var Survey = result.FirstOrDefault(o => o.Id == id);

                if (Survey == null)
                {
                    return NotFound();
                }

                return Ok(Survey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Survey/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateSurvey/{id}")]
        public async Task<IActionResult> PutSurvey(string accessToken, Guid id, Survey Survey)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Survey.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Survey).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyExists(id))
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

        // POST: api/Survey
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateSurvey")]
        public async Task<ActionResult<Survey>> PostSurvey(string accessToken, Survey Survey)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Survey.Id = Guid.NewGuid();
                Survey.Created = DateTime.UtcNow;
                Survey.DateCreated = DateTime.UtcNow;
                _context.Surveys.Add(Survey);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSurvey", new { id = Survey.Id }, Survey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Survey/5
        [HttpDelete("DeleteSurvey/{id}")]
        public async Task<ActionResult<Survey>> DeleteSurvey(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Survey = await _context.Surveys.FindAsync(id);
                if (Survey == null)
                {
                    return NotFound();
                }
                Survey.Deleted = DateTime.UtcNow;
                _context.Entry(Survey).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Survey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool SurveyExists(Guid id)
        {
            return _context.Surveys.Any(e => e.Id == id);
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
