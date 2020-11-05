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
    //[Clientesize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<ClientesController> _logger;
        private readonly JWTSettings _jwtsettings;
        public ClientesController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<ClientesController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Clientes
        [HttpGet("GetClientes")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            try
            {
                var result = await _context.Clientes.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetClientesCount")]
        public async Task<ActionResult<ItemCount>> GetClientesCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.Clientes.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Clientes
        [HttpGet("GetClientesByPage")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientesByPage(int pageSize, int pageNumber)
        {

            List<Cliente> ClientesList = await _context.Clientes.Where(o => o.Deleted == null).ToListAsync();
            ClientesList = ClientesList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(ClientesList);
        }

        // GET: api/Clientes/5
        [HttpGet("GetClientes/{id}")]
        public async Task<ActionResult<Cliente>> GetClientes(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Clientes = await _context.Clientes.FindAsync(id);

                if (Clientes == null)
                {
                    return NotFound();
                }

                return Ok(Clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateClientes/{id}")]
        public async Task<IActionResult> PutClientes(string accessToken, Guid id, Cliente Clientes)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != Clientes.Id)
                {
                    return BadRequest();
                }

                _context.Entry(Clientes).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateClientes")]
        public async Task<ActionResult<Cliente>> PostClientes(string accessToken, Cliente Clientes)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                Clientes.Id = Guid.NewGuid();
                Clientes.Created = DateTime.UtcNow;
                _context.Clientes.Add(Clientes);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetClientes", new { id = Clientes.Id }, Clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/Clientes/5
        [HttpDelete("DeleteClientes/{id}")]
        public async Task<ActionResult<Cliente>> DeleteClientes(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var Clientes = await _context.Clientes.FindAsync(id);
                if (Clientes == null)
                {
                    return NotFound();
                }
                Clientes.Deleted = DateTime.UtcNow;
                _context.Entry(Clientes).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(Clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool ClientesExists(Guid id)
        {
            return _context.Clientes.Any(e => e.Id == id);
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
