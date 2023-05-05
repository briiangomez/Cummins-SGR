using TBBlazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SGR.Models.Models;

namespace TBBlazorApp.Interfaces
{
    public interface ILoginService
    {
        public Task<UserModel> LoginAsync(UserModel user);
        public Task<UserModel> RegisterUserAsync(User user);
        public Task<User> GetUserByAccessTokenAsync(string accessToken);
        public Task<UserModel> RefreshTokenAsync(RefreshRequest refreshRequest);
    }
}
