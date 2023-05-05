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
    //[Oemsize]
    [Route("api/[controller]")]
    [ApiController]
    public class OemsController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<OemsController> _logger;
        private readonly JWTSettings _jwtsettings;
        public OemsController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<OemsController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Oems
        [HttpGet("GetOems")]
        public async Task<ActionResult<IEnumerable<Oem>>> GetOemss()
        {
            try
            {
                var result = await _context.Oems.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetOemsCount")]
        public async Task<ActionResult<ItemCount>> GetOemsCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Oems.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Oems
        [HttpGet("GetOemsByPage")]
        public async Task<ActionResult<IEnumerable<Oem>>> GetOemsByPage(int pageSize, int pageNumber)
        {

            List<Oem> OemsList = await _context.Oems.Where(o => o.Deleted == null).ToListAsync();
            OemsList = OemsList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(OemsList);
        }

        // GET: api/Oems/5
        [HttpGet("GetOems/{id}")]
        public async Task<ActionResult<Oem>> GetOems(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Oems = await _context.Oems.FindAsync(id);

                if (Oems == null)
                {
                    return NotFound();
                }

                return Ok(Oems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Oems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateOems/{id}")]
        public async Task<IActionResult> PutOems(string accessToken, Guid id, Oem Oems)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Oems.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Oems).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OemsExists(id))
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

        // POST: api/Oems
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateOems")]
        public async Task<ActionResult<Oem>> PostOems(string accessToken, Oem Oems)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Oems.Id = Guid.NewGuid();
                Oems.Created = DateTime.UtcNow;
                _context.Oems.Add(Oems);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetOems", new { id = Oems.Id }, Oems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Oems/5
        [HttpDelete("DeleteOems/{id}")]
        public async Task<ActionResult<Oem>> DeleteOems(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Oems = await _context.Oems.FindAsync(id);
                if (Oems == null)
                {
                    return NotFound();
                }
                Oems.Deleted = DateTime.UtcNow;
                _context.Entry(Oems).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Oems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool OemsExists(Guid id)
        {
            return _context.Oems.Any(e => e.Id == id);
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
