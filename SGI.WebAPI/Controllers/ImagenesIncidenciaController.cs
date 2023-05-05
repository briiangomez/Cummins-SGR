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
    //[ImagenesIncidenciaize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesIncidenciaController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly ILogger<ImagenesIncidenciaController> _logger;
        private readonly JWTSettings _jwtsettings;
        public ImagenesIncidenciaController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<ImagenesIncidenciaController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/ImagenesIncidencia
        [HttpGet("GetImagenesIncidencia")]
        public async Task<ActionResult<IEnumerable<ImagenesIncidencium>>> GetImagenesIncidencia()
        {
            try
            {
                var result = await _context.ImagenesIncidencia.Where(o => o.Deleted == null).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetImagenesIncidenciasById/{id}")]
        public async Task<ActionResult<IEnumerable<ImagenesIncidencium>>> GetImagenesIncidencia(Guid id)
        {
            try
            {
                var result = await _context.ImagenesIncidencia.Where(o => o.Deleted == null && o.IncidenciaId == id).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetImagenesIncidenciaCount")]
        public async Task<ActionResult<ItemCount>> GetImagenesIncidenciaCount(string accessToken)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                ItemCount itemCount = new ItemCount();

                itemCount.Count = _context.ImagenesIncidencia.Where(o => o.Deleted == null).Count();
                var result = await Task.FromResult(itemCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/ImagenesIncidencia
        [HttpGet("GetImagenesIncidenciaByPage")]
        public async Task<ActionResult<IEnumerable<ImagenesIncidencium>>> GetImagenesIncidenciaByPage(int pageSize, int pageNumber)
        {

            List<ImagenesIncidencium> ImagenesIncidenciaList = await _context.ImagenesIncidencia.Where(o => o.Deleted == null).ToListAsync();
            ImagenesIncidenciaList = ImagenesIncidenciaList.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            return await Task.FromResult(ImagenesIncidenciaList);
        }

        // GET: api/ImagenesIncidencia/5
        [HttpGet("GetImagenesIncidencia/{id}")]
        public async Task<ActionResult<ImagenesIncidencium>> GetImagenesIncidencia(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var ImagenesIncidencia = await _context.ImagenesIncidencia.FindAsync(id);

                if (ImagenesIncidencia == null)
                {
                    return NotFound();
                }

                return Ok(ImagenesIncidencia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // PUT: api/ImagenesIncidencia/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateImagenesIncidencia/{id}")]
        public async Task<IActionResult> PutImagenesIncidencia(string accessToken, Guid id, ImagenesIncidencium ImagenesIncidencia)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                if (id != ImagenesIncidencia.Id)
                {
                    return BadRequest();
                }

                _context.Entry(ImagenesIncidencia).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagenesIncidenciaExists(id))
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

        // POST: api/ImagenesIncidencia
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateImagenesIncidencia")]
        public async Task<ActionResult<ImagenesIncidencium>> PostImagenesIncidencia(string accessToken, ImagenesIncidencium ImagenesIncidencia)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");

                //var result = _context.ImagenesIncidencias.Where(o => o.IncidenciaId == ImagenesIncidencia.IncidenciaId)
                //    .ToList();

                //foreach (var item in result)
                //{
                //    _context.ImagenesIncidencias.Remove(item);
                //}

                ImagenesIncidencia.Id = Guid.NewGuid();
                ImagenesIncidencia.Created = DateTime.UtcNow;
                _context.ImagenesIncidencia.Add(ImagenesIncidencia);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetImagenesIncidencia", new { id = ImagenesIncidencia.Id }, ImagenesIncidencia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // DELETE: api/ImagenesIncidencia/5
        [HttpDelete("DeleteImagenesIncidencia/{id}")]
        public async Task<ActionResult<ImagenesIncidencium>> DeleteImagenesIncidencia(string accessToken, Guid id)
        {
            try
            {
                if (GetUserFromAccessToken(accessToken))
                    throw new Exception("Invalid Token");
                var ImagenesIncidencia = await _context.ImagenesIncidencia.FindAsync(id);
                if (ImagenesIncidencia == null)
                {
                    return NotFound();
                }
                ImagenesIncidencia.Deleted = DateTime.UtcNow;
                _context.Entry(ImagenesIncidencia).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(ImagenesIncidencia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool ImagenesIncidenciaExists(Guid id)
        {
            return _context.ImagenesIncidencia.Any(e => e.Id == id);
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
