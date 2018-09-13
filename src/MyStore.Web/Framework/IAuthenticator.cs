using System.Threading.Tasks;

namespace MyStore.Web.Framework
{
    public interface IAuthenticator
    {
        Task SignInAsync(string username, string password);
        Task SignOutAsync();
    }
}