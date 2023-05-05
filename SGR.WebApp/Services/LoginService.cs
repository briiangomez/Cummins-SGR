using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookStoreWebAPI;
using TBBlazorApp.Interfaces;
using SGR.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TBBlazorApp.Data;

namespace TBBlazorApp.Services
{
    public class LoginService : ILoginService
    {
        private SGIDbContext _db;

        private JWTSettings _jwtsettings;
        public LoginService(SGIDbContext db, IOptions<JWTSettings> jwtsettings)
        {
            _jwtsettings = jwtsettings.Value;
            _db = db;
        }

        public async Task<UserModel> LoginAsync(UserModel user)
        {
            user.Password = Utilities.Encrypt(user.Password);
            var users = await _db.Users.Include(u => u.IdRoleNavigation)
                            .Where(u => u.EmailAddress == user.EmailAddress
                                    && u.Password == user.Password).FirstOrDefaultAsync();



            UserModel userWithToken = null;

            if (user != null)
            {
                RefreshToken refreshToken = GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _db.SaveChangesAsync();

                userWithToken = new UserModel(users);
                userWithToken.RefreshToken = refreshToken.Token;
            }

            if (userWithToken == null)
            {
                return null;
            }

            //sign your token here here..
            userWithToken.AccessToken = GenerateAccessToken(user.Id);

            return await Task.FromResult(userWithToken);

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

        public async Task<UserModel> RegisterUserAsync(User user)
        {
            user.Password = Utilities.Encrypt(user.Password);
            user.Created = DateTime.Now;
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var users = await _db.Users.Include(u => u.IdRoleNavigation)
                                            .Where(u => u.Id == user.Id).FirstOrDefaultAsync();


            UserModel userWithToken = null;

            if (user != null)
            {
                RefreshToken refreshToken = GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _db.SaveChangesAsync();

                userWithToken = new UserModel(users);
                userWithToken.RefreshToken = refreshToken.Token;
            }

            if (userWithToken == null)
            {
                return null;
            }

            //sign your token here here..
            userWithToken.AccessToken = GenerateAccessToken(user.Id);

            return await Task.FromResult(userWithToken);
        }

        public async Task<UserModel> RefreshTokenAsync(RefreshRequest refreshRequest)
        {
            User user = await GetUserFromAccessToken(refreshRequest.AccessToken);

            if (user != null && ValidateRefreshToken(user, refreshRequest.RefreshToken))
            {
                UserModel userWithToken = new UserModel(user);
                userWithToken.AccessToken = GenerateAccessToken(user.Id);

                return await Task.FromResult(userWithToken);
            }
            return null;
        }

        private bool ValidateRefreshToken(User user, string refreshToken)
        {

            RefreshToken refreshTokenUser = _db.RefreshTokens.Where(rt => rt.Token == refreshToken)
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

                    return await _db.Users.Include(u => u.IdRoleNavigation).Include(u => u.IdDealerNavigation)
                                        .Where(u => u.Id == Guid.Parse(userId)).FirstOrDefaultAsync();
                }
            }
            catch (Exception)
            {
                return new User();
            }

            return new User();
        }

        public async Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            User user = await GetUserFromAccessToken(accessToken);

            if (user != null)
            {
                return await Task.FromResult(user);
            }

            return null;
        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

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
    }
}
