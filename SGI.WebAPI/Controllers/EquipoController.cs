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
    //[Motorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<EquipoController> _logger;
        private readonly JWTSettings _jwtsettings;
        public EquipoController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<EquipoController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Motor
        [HttpGet("GetEquipo")]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetMotor(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var result = await _context.Equipos.Where(o => o.Deleted == null).Include(o => o.Oem).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Motor
        [HttpGet("GetEquipoFull")]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetMotorFull(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var result = await _context.Equipos.Where(o => o.Deleted == null).Include(o => o.Oem).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetEquipoCount")]
        public async Task<ActionResult<ItemCount>> GetMotorCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Equipos.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Motor
        [HttpGet("GetEquipoByPage")]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetMotorByPage(int pageSize, int pageNumber)
        {

            List<Equipo> MotorList = await _context.Equipos.Where(o => o.Deleted == null).ToListAsync();
            MotorList = MotorList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(MotorList);
        }

        // GET: api/Motor/5
        [HttpGet("GetEquipo/{id}")]
        public async Task<ActionResult<Equipo>> GetMotor(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                //var result = await _context.Equipos.Where(o => o.Deleted == null).Include(o => o.Oem).Include(o => o.Motor).ToListAsync();

                var Motor = _context.Equipos.FirstOrDefault(o => o.Id == id);

                if (Motor == null)
                {
                    return NotFound();
                }

                return Ok(Motor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
            
        }

        // PUT: api/Motor/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateEquipo/{id}")]
        public async Task<IActionResult> PutMotor(string accessToken, Guid id, Equipo Motor)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Motor.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Motor).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotorExists(id))
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

        // POST: api/Motor
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateEquipo")]
        public async Task<ActionResult<Motor>> PostMotor(string accessToken, Equipo Motor)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Motor.Id = Guid.NewGuid();
                Motor.Created = DateTime.UtcNow;
                _context.Equipos.Add(Motor);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMotor", new { id = Motor.Id }, Motor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Motor/5
        [HttpDelete("DeleteEquipo/{id}")]
        public async Task<ActionResult<Equipo>> DeleteMotor(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Motor = await _context.Equipos.FindAsync(id);
                if (Motor == null)
                {
                    return NotFound();
                }
                Motor.Deleted = DateTime.UtcNow;
                _context.Entry(Motor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Motor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
            
        }

        private bool MotorExists(Guid id)
        {
            return _context.Equipos.Any(e => e.Id == id);
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
