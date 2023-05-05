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
    //[SurveyItemize]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyItemController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<SurveyItemController> _logger;
        private readonly JWTSettings _jwtsettings;
        public SurveyItemController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<SurveyItemController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/SurveyItem
        [HttpGet("GetSurveyItem")]
        public async Task<ActionResult<IEnumerable<SurveyItem>>> GetSurveyItems()
        {
            try
            {
                var result = await _context.SurveyItems.Where(o => o.Deleted == null).Include(x => x.SurveyItemOptions).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetSurveyItemBySurvey/{SurveyId}")]
        public async Task<ActionResult<IEnumerable<SurveyItem>>> GetSurveyItemsBySurvey(Guid SurveyId)
        {
            try
            {
                var result = await _context.SurveyItems.Where(o => o.Deleted == null && o.Survey == SurveyId).Include(x => x.SurveyItemOptions)
.ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetSurveyItemCount")]
        public async Task<ActionResult<ItemCount>> GetSurveyItemCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.SurveyItems.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/SurveyItem
        [HttpGet("GetSurveyItemByPage")]
        public async Task<ActionResult<IEnumerable<SurveyItem>>> GetSurveyItemByPage(int pageSize, int pageNumber)
        {

            List<SurveyItem> SurveyItemList = await _context.SurveyItems.Where(o => o.Deleted == null).Include(x => x.SurveyItemOptions)
.ToListAsync();
            SurveyItemList = SurveyItemList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(SurveyItemList);
        }

        // GET: api/SurveyItem/5
        [HttpGet("GetSurveyItem/{id}")]
        public async Task<ActionResult<SurveyItem>> GetSurveyItem(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var result = await _context.SurveyItems.Where(o => o.Deleted == null).Include(x => x.SurveyItemOptions).ToListAsync();

                var SurveyItem = result.FirstOrDefault(o => o.Id == id);

                if (SurveyItem == null)
                {
                    return NotFound();
                }

                return Ok(SurveyItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/SurveyItem/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateSurveyItem/{id}")]
        public async Task<IActionResult> PutSurveyItem(string accessToken, Guid id, SurveyItem SurveyItem)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != SurveyItem.Id)
                {
                    return BadRequest();
                }

                var ExistingSurveyItem = _context.SurveyItems
                                     .Where(x => x.Id == SurveyItem.Id)
                                     .Include(x => x.SurveyItemOptions)
                                     .FirstOrDefault();

                ExistingSurveyItem.ItemLabel = SurveyItem.ItemLabel;
                ExistingSurveyItem.ItemType = SurveyItem.ItemType;
                ExistingSurveyItem.ItemValue = SurveyItem.ItemValue;
                ExistingSurveyItem.Required = SurveyItem.Required;
                ExistingSurveyItem.Position = SurveyItem.Position;
                ExistingSurveyItem.SurveyItemOptions = SurveyItem.SurveyItemOptions;

                _context.SaveChanges();

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyItemExists(id))
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

        // POST: api/SurveyItem
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateSurveyItem")]
        public async Task<ActionResult<SurveyItem>> PostSurveyItem(string accessToken, SurveyItem SurveyItem)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                SurveyItem.Id = Guid.NewGuid();
                SurveyItem.Created = DateTime.UtcNow;
                // Set position
                int CoutOfSurveyItems =
                    _context.SurveyItems
                    .Where(x => x.Survey == SurveyItem.Survey)
                    .Count();

                SurveyItem.Position = CoutOfSurveyItems + 1;
                _context.SurveyItems.Add(SurveyItem);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSurveyItem", new { id = SurveyItem.Id }, SurveyItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/SurveyItem/5
        [HttpDelete("DeleteSurveyItem/{id}")]
        public async Task<ActionResult<SurveyItem>> DeleteSurveyItem(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var SurveyItem = await _context.SurveyItems.FindAsync(id);
                if (SurveyItem == null)
                {
                    return NotFound();
                }
                SurveyItem.Deleted = DateTime.UtcNow;
                _context.Entry(SurveyItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(SurveyItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool SurveyItemExists(Guid id)
        {
            return _context.SurveyItems.Any(e => e.Id == id);
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
