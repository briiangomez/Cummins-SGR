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
    //[Dealerize]
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<DealerController> _logger;
        private readonly JWTSettings _jwtsettings;
        public DealerController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<DealerController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Dealer
        [HttpGet("GetDealer")]
        public async Task<ActionResult<IEnumerable<Dealer>>> GetDealers()
        {
            try
            {
                var result = await _context.Dealers.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetDealerCount")]
        public async Task<ActionResult<ItemCount>> GetDealerCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Dealers.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Dealer
        [HttpGet("GetDealerByPage")]
        public async Task<ActionResult<IEnumerable<Dealer>>> GetDealerByPage(int pageSize, int pageNumber)
        {

            List<Dealer> DealerList = await _context.Dealers.Where(o => o.Deleted == null).ToListAsync();
            DealerList = DealerList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(DealerList);
        }

        // GET: api/Dealer/5
        [HttpGet("GetDealer/{id}")]
        public async Task<ActionResult<Dealer>> GetDealer(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Dealer = await _context.Dealers.FindAsync(id);

                if (Dealer == null)
                {
                    return NotFound();
                }

                return Ok(Dealer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Dealer/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateDealer/{id}")]
        public async Task<IActionResult> PutDealer(string accessToken, Guid id, Dealer Dealer)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Dealer.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Dealer).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealerExists(id))
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

        // POST: api/Dealer
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateDealer")]
        public async Task<ActionResult<Dealer>> PostDealer(string accessToken, Dealer Dealer)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Dealer.Id = Guid.NewGuid();
                Dealer.Created = DateTime.UtcNow;
                _context.Dealers.Add(Dealer);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDealer", new { id = Dealer.Id }, Dealer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Dealer/5
        [HttpDelete("DeleteDealer/{id}")]
        public async Task<ActionResult<Dealer>> DeleteDealer(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Dealer = await _context.Dealers.FindAsync(id);
                if (Dealer == null)
                {
                    return NotFound();
                }
                Dealer.Deleted = DateTime.UtcNow;
                _context.Entry(Dealer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Dealer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool DealerExists(Guid id)
        {
            return _context.Dealers.Any(e => e.Id == id);
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
