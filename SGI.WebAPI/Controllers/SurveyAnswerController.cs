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
    //[SurveyAnswerize]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyAnswerController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<SurveyAnswerController> _logger;
        private readonly JWTSettings _jwtsettings;
        public SurveyAnswerController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<SurveyAnswerController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/SurveyAnswer
        [HttpGet("GetSurveyAnswer")]
        public async Task<ActionResult<IEnumerable<SurveyAnswer>>> GetSurveyAnswers()
        {
            try
            {
                var result = await _context.SurveyAnswers.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetSurveyAnswerCount")]
        public async Task<ActionResult<ItemCount>> GetSurveyAnswerCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.SurveyAnswers.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/SurveyAnswer
        [HttpGet("GetSurveyAnswerByPage")]
        public async Task<ActionResult<IEnumerable<SurveyAnswer>>> GetSurveyAnswerByPage(int pageSize, int pageNumber)
        {

            List<SurveyAnswer> SurveyAnswerList = await _context.SurveyAnswers.Where(o => o.Deleted == null).ToListAsync();
            SurveyAnswerList = SurveyAnswerList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(SurveyAnswerList);
        }

        // GET: api/SurveyAnswer/5
        [HttpGet("GetSurveyAnswer/{id}")]
        public async Task<ActionResult<SurveyAnswer>> GetSurveyAnswer(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var SurveyAnswer = await _context.SurveyAnswers.FindAsync(id);

                if (SurveyAnswer == null)
                {
                    return NotFound();
                }

                return Ok(SurveyAnswer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/SurveyAnswer/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateSurveyAnswer/{id}")]
        public async Task<IActionResult> PutSurveyAnswer(string accessToken, Guid id, SurveyAnswer SurveyAnswer)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != SurveyAnswer.Id)
                {
                    return BadRequest();
                }

                _context.Entry(SurveyAnswer).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyAnswerExists(id))
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

        // POST: api/SurveyAnswer
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateSurveyAnswer")]
        public async Task<ActionResult<SurveyAnswer>> PostSurveyAnswer(string accessToken, DTOSurvey paramDTOSurvey)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");

                List<SurveyAnswer> SurveyAnswers = new List<SurveyAnswer>();

                foreach (var SurveyItem in paramDTOSurvey.SurveyItem)
                {
                    // Delete possible existing answer
                    var ExistingAnswers = _context.SurveyAnswers
                        .Where(x => x.SurveyItemId == SurveyItem.Id)
                        .Where(x => x.UserId == paramDTOSurvey.UserId)
                        .ToList();

                    if (ExistingAnswers != null)
                    {
                        _context.SurveyAnswers.RemoveRange(ExistingAnswers);
                        _context.SaveChanges();
                    }

                    // Save Answer

                    if (SurveyItem.ItemType != "Multi-Select Dropdown")
                    {
                        SurveyAnswer NewSurveyAnswer = new SurveyAnswer();

                        NewSurveyAnswer.AnswerValue = SurveyItem.AnswerValueString;
                        NewSurveyAnswer.AnswerValueDateTime = SurveyItem.AnswerValueDateTime;
                        NewSurveyAnswer.SurveyItemId = SurveyItem.Id;
                        NewSurveyAnswer.UserId = paramDTOSurvey.UserId;

                        _context.SurveyAnswers.Add(NewSurveyAnswer);
                        _context.SaveChanges();
                    }

                    if (SurveyItem.AnswerValueList != null)
                    {
                        foreach (var item in SurveyItem.AnswerValueList)
                        {
                            SurveyAnswer NewSurveyAnswerValueList = new SurveyAnswer();

                            NewSurveyAnswerValueList.AnswerValue = item;
                            NewSurveyAnswerValueList.SurveyItemId = SurveyItem.Id;
                            NewSurveyAnswerValueList.UserId = paramDTOSurvey.UserId;

                            _context.SurveyAnswers.Add(NewSurveyAnswerValueList);
                            _context.SaveChanges();
                        }
                    }
                }


                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/SurveyAnswer/5
        [HttpDelete("DeleteSurveyAnswer/{id}")]
        public async Task<ActionResult<SurveyAnswer>> DeleteSurveyAnswer(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var SurveyAnswer = await _context.SurveyAnswers.FindAsync(id);
                if (SurveyAnswer == null)
                {
                    return NotFound();
                }
                SurveyAnswer.Deleted = DateTime.UtcNow;
                _context.Entry(SurveyAnswer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(SurveyAnswer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool SurveyAnswerExists(Guid id)
        {
            return _context.SurveyAnswers.Any(e => e.Id == id);
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
