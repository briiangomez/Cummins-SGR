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
    //[Roleize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<RoleController> _logger;
        private readonly JWTSettings _jwtsettings;
        public RoleController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<RoleController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Role
        [HttpGet("GetRole")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var result = await _context.Roles.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetRoleCount")]
        public async Task<ActionResult<ItemCount>> GetRoleCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Roles.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Role
        [HttpGet("GetRoleByPage")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoleByPage(int pageSize, int pageNumber)
        {

            List<Role> RoleList = await _context.Roles.Where(o => o.Deleted == null).ToListAsync();
            RoleList = RoleList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(RoleList);
        }

        // GET: api/Role/5
        [HttpGet("GetRole/{id}")]
        public async Task<ActionResult<Role>> GetRole(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Role = await _context.Roles.FindAsync(id);

                if (Role == null)
                {
                    return NotFound();
                }

                return Ok(Role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Role/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateRole/{id}")]
        public async Task<IActionResult> PutRole(string accessToken, Guid id, Role Role)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Role.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Role).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(id))
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

        // POST: api/Role
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateRole")]
        public async Task<ActionResult<Role>> PostRole(string accessToken, Role Role)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Role.Id = Guid.NewGuid();
                Role.Created = DateTime.UtcNow;
                _context.Roles.Add(Role);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetRole", new { id = Role.Id }, Role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Role/5
        [HttpDelete("DeleteRole/{id}")]
        public async Task<ActionResult<Role>> DeleteRole(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Role = await _context.Roles.FindAsync(id);
                if (Role == null)
                {
                    return NotFound();
                }
                Role.Deleted = DateTime.UtcNow;
                _context.Entry(Role).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool RoleExists(Guid id)
        {
            return _context.Roles.Any(e => e.Id == id);
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
