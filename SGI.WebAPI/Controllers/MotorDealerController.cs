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
    //[MotorDealerize]
    [Route("api/[controller]")]
    [ApiController]
    public class MotorDealerController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<MotorDealerController> _logger;
        private readonly JWTSettings _jwtsettings;
        public MotorDealerController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<MotorDealerController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/MotorDealer
        [HttpGet("GetMotorDealer")]
        public async Task<ActionResult<IEnumerable<MotorDealer>>> GetMotorDealer()
        {
            try
            {
                var result = await _context.MotorDealers.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetMotorDealerByDealer/{id}")]
        public async Task<ActionResult<IEnumerable<MotorDealer>>> GetMotorDealerByDealer(Guid id)
        {
            try
            {
                var result = await _context.MotorDealers.Where(o => o.Deleted == null && o.DealerId == id).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetMotorDealerCount")]
        public async Task<ActionResult<ItemCount>> GetMotorDealerCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.MotorDealers.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/MotorDealer
        [HttpGet("GetMotorDealerByPage")]
        public async Task<ActionResult<IEnumerable<MotorDealer>>> GetMotorDealerByPage(int pageSize, int pageNumber)
        {
            List<MotorDealer> dealerss = new List<MotorDealer>();
            List<Dealer> MotorDealerList = await _context.Dealers.Where(o => o.Deleted == null).ToListAsync();
            List<Motor> motorList = await _context.Motors.Where(o => o.Deleted == null).ToListAsync();

            try
            {
                foreach (var dealer in MotorDealerList)
                {
                    foreach (var item in motorList)
                    {
                        MotorDealer cert = new MotorDealer();
                        cert.DealerId = dealer.Id;
                        cert.MotorId = item.Id;
                        _context.MotorDealers.Add(cert);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return await Task.FromResult(dealerss);
        }

        // GET: api/MotorDealer/5
        [HttpGet("GetMotorDealer/{id}")]
        public async Task<ActionResult<MotorDealer>> GetMotorDealer(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var MotorDealer = await _context.MotorDealers.FindAsync(id);

                if (MotorDealer == null)
                {
                    return NotFound();
                }

                return Ok(MotorDealer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/MotorDealer/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateMotorDealer/{id}")]
        public async Task<IActionResult> PutMotorDealer(string accessToken, Guid id, MotorDealer MotorDealer)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != MotorDealer.Id)
                {
                    return BadRequest();
                }

                _context.Entry(MotorDealer).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotorDealerExists(id))
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

        // POST: api/MotorDealer
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateMotorDealer")]
        public async Task<ActionResult<MotorDealer>> PostMotorDealer(string accessToken, MotorDealer MotorDealer)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                MotorDealer.Id = Guid.NewGuid();
                MotorDealer.Created = DateTime.UtcNow;
                _context.MotorDealers.Add(MotorDealer);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMotorDealer", new { id = MotorDealer.Id }, MotorDealer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/MotorDealer/5
        [HttpDelete("DeleteMotorDealer/{id}")]
        public async Task<ActionResult<MotorDealer>> DeleteMotorDealer(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var MotorDealer = await _context.MotorDealers.FindAsync(id);
                if (MotorDealer == null)
                {
                    return NotFound();
                }
                MotorDealer.Deleted = DateTime.UtcNow;
                _context.Entry(MotorDealer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(MotorDealer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool MotorDealerExists(Guid id)
        {
            return _context.MotorDealers.Any(e => e.Id == id);
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
