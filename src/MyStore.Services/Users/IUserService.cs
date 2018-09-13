using System.Threading.Tasks;
using MyStore.Infrastructure.Jwt;

namespace MyStore.Services.Users
{
    public interface IUserService
    {
        Task<string> GetRoleAsync(string username);
        Task SignUpAsync(string username, string password, string role);
        Task SignInAsync(string username, string password);
        Task<JsonWebToken> CreateTokenAsync(string username);
    }
}