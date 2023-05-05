using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SGIWebApi.Models;
using SGR.Models.Models;

namespace SGIWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<SettingsController> _logger;
        private readonly JWTSettings _jwtsettings;
        public SettingsController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<SettingsController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Settings
        [HttpGet("GetSettings")]
        public async Task<ActionResult<IEnumerable<Setting>>> GetSettingss()
        {
            try
            {
                var result = await _context.Settings.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetSettingsCount")]
        public async Task<ActionResult<ItemCount>> GetSettingsCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Settings.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Settings
        [HttpGet("GetSettingsByPage")]
        public async Task<ActionResult<IEnumerable<Setting>>> GetSettingsByPage(int pageSize, int pageNumber)
        {

            List<Setting> SettingsList = await _context.Settings.Where(o => o.Deleted == null).ToListAsync();
            SettingsList = SettingsList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(SettingsList);
        }

        // GET: api/Settings/5
        [HttpGet("GetSettings/{id}")]
        public async Task<ActionResult<Setting>> GetSettings(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Settings = await _context.Settings.FindAsync(id);

                if (Settings == null)
                {
                    return NotFound();
                }

                return Ok(Settings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Settings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateSettings/{id}")]
        public async Task<IActionResult> PutSettings(string accessToken, Guid id, Setting Settings)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Settings.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Settings).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingsExists(id))
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

        // POST: api/Settings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateSettings")]
        public async Task<ActionResult<Setting>> PostSettings(string accessToken, Setting Settings)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Settings.Id = Guid.NewGuid();
                Settings.Created = DateTime.UtcNow;
                _context.Settings.Add(Settings);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSettings", new { id = Settings.Id }, Settings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Settings/5
        [HttpDelete("DeleteSettings/{id}")]
        public async Task<ActionResult<Setting>> DeleteSettings(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Settings = await _context.Settings.FindAsync(id);
                if (Settings == null)
                {
                    return NotFound();
                }
                Settings.Deleted = DateTime.UtcNow;
                _context.Entry(Settings).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Settings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool SettingsExists(Guid id)
        {
            return _context.Settings.Any(e => e.Id == id);
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