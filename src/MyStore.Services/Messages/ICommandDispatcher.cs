using System.Threading.Tasks;

namespace MyStore.Services.Messages
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}