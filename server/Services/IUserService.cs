using Sgi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sgi.Services
{
    public interface IUserService
    {
        public Task<User> LoginAsync(User user);
        public Task<User> RegisterUserAsync(User user);
        public Task<User> GetUserByAccessTokenAsync(string accessToken);
        public Task<User> RefreshTokenAsync(RefreshRequest refreshRequest);
    }
}
