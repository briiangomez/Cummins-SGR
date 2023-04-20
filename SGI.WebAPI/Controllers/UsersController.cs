using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SGIWebApi.Models;
using SGR.Models;
using SGR.Models.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SGIWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SGIDbContext _context;
        private readonly JWTSettings _jwtsettings;
        private readonly ILogger<DealerController> _logger;

        public UsersController(SGIDbContext context, IOptions<JWTSettings> jwtsettings, ILogger<DealerController> logger)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {

            try
            {
                var result = await _context.Users.Include(o => o.IdRoleNavigation).Include(o => o.IdDealerNavigation).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Users
        [HttpGet("GetUsersFull")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersFull()
        {

            try
            {
                var result = await _context.Users.Include(o => o.IdRoleNavigation).Include(o => o.IdDealerNavigation).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Users/5
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {

            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        // GET: api/Users/5
        [HttpGet("GetUserDetails/{id}")]
        public async Task<ActionResult<User>> GetUserDetails(Guid id)
        {
            try
            {
                var user = await _context.Users.Include(u => u.IdRoleNavigation)
                                            .Where(u => u.Id == id)
                                            .FirstOrDefaultAsync();

                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // POST: api/Users
        [HttpPost("Login")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<UserWithToken>> Login([FromBody] User user)
        {
            try
            {
                user = await _context.Users.Include(u => u.IdRoleNavigation)
                            .Where(u => u.EmailAddress == user.EmailAddress
                                    && u.Password == user.Password && u.Deleted == null).FirstOrDefaultAsync();

                if(user != null)
                {
                    Logger.AddLine(String.Format("{0}-{1}-{2}", DateTime.Now.ToString(), user.EmailAddress, user.Password));
                }
                else
                {
                    Logger.AddLine(String.Format("{0}-{1}-{2}", DateTime.Now.ToString(), "DATABASE RETURN ", " USER DATA NULL"));
                }

                UserWithToken userWithToken = null;

                if (user != null)
                {
                    RefreshToken refreshToken = GenerateRefreshToken();
                    refreshToken.IdUser = user.Id;
                    user.RefreshTokens.Add(refreshToken);
                    await _context.SaveChangesAsync();

                    userWithToken = new UserWithToken(user);
                    userWithToken.RefreshToken = refreshToken.Token;
                }

                if (userWithToken == null)
                {
                    return NotFound();
                }

                //sign your token here here..
                userWithToken.AccessToken = GenerateAccessToken(user.Id);
                return Ok(userWithToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                Logger.AddLine(String.Format("{0}-{1}-{2}", DateTime.Now.ToString(), ex.Message,ex.StackTrace));
                return BadRequest(ex);
            }

        }

        // POST: api/Users
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<UserWithToken>> RegisterUser([FromBody] User user)
        {
            try
            {
                user.Created = DateTime.Now;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();


                //load role for registered user
                user = await _context.Users.Include(u => u.IdRoleNavigation)
                                            .Where(u => u.Id == user.Id).FirstOrDefaultAsync();

                UserWithToken userWithToken = null;

                if (user != null)
                {
                    RefreshToken refreshToken = GenerateRefreshToken();
                    user.RefreshTokens.Add(refreshToken);
                    await _context.SaveChangesAsync();

                    userWithToken = new UserWithToken(user);
                    userWithToken.RefreshToken = refreshToken.Token;
                }

                if (userWithToken == null)
                {
                    return NotFound();
                }

                //sign your token here here..
                userWithToken.AccessToken = GenerateAccessToken(user.Id);
                return Ok(userWithToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Users
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<UserWithToken>> RefreshToken([FromBody] RefreshRequest refreshRequest)
        {
            try
            {
                User user = await GetUserFromAccessToken(refreshRequest.AccessToken);

                if (user != null && ValidateRefreshToken(user, refreshRequest.RefreshToken))
                {
                    UserWithToken userWithToken = new UserWithToken(user);
                    userWithToken.AccessToken = GenerateAccessToken(user.Id);
                    userWithToken.IdOem = user.IdOem;
                    userWithToken.IdDealer = user.IdDealer;
                    userWithToken.IdOemNavigation = user.IdOemNavigation;
                    userWithToken.IdDealerNavigation = user.IdDealerNavigation;
                    return Ok(userWithToken);
                }
                return null;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // GET: api/Users
        [HttpPost("GetUserByAccessToken")]
        public async Task<ActionResult<User>> GetUserByAccessToken([FromBody] string accessToken)
        {
            try
            {
                User user = await GetUserFromAccessToken(accessToken);

                if (user != null)
                {
                    return user;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        private bool ValidateRefreshToken(User user, string refreshToken)
        {

            RefreshToken refreshTokenUser = _context.RefreshTokens.Where(rt => rt.Token == refreshToken)
                                                .OrderByDescending(rt => rt.ExpiryDate)
                                                .FirstOrDefault();

            if (refreshTokenUser != null && refreshTokenUser.IdUser == user.Id
                && refreshTokenUser.ExpiryDate > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }

        private async Task<User> GetUserFromAccessToken(string accessToken)
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

                    return await _context.Users.Include(u => u.IdRoleNavigation).Include(u => u.IdDealerNavigation)
                                        .Where(u => u.Id == Guid.Parse(userId)).FirstOrDefaultAsync();
                }
            }
            catch (Exception)
            {
                return new User();
            }

            return new User();
        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();
            //refreshToken.Id = Guid.NewGuid();
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);
            refreshToken.Created = DateTime.UtcNow;
            return refreshToken;
        }

        private string GenerateAccessToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            try
            {

                if (id != user.Id)
                {
                    return BadRequest();
                }

                _context.Entry(user).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            try
            {
                var User = await _context.Users.FindAsync(id);
                if (User == null)
                {
                    return NotFound();
                }
                User.Deleted = DateTime.Now;
                _context.Entry(User).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(User);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }
        }

        // DELETE: api/User/5
        [HttpDelete("ActivateUser/{id}")]
        public async Task<ActionResult<User>> ActivateUser(string accessToken, Guid id)
        {
            try
            {
                var User = await _context.Users.FindAsync(id);
                if (User == null)
                {
                    return NotFound();
                }
                User.Deleted = null;
                _context.Entry(User).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(User);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex);
            }

        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
