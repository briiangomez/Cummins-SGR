using System.Threading.Tasks;
using SGI.Infrastructure.Entities;

namespace SGI.Infrastructure.Interfaces
{
    public interface IAuthService
    {
        Task<Token> LoginAsync(string userName, string password);

        Task<Token> RefreshTokenAsync(string refreshToken);
    }
}
