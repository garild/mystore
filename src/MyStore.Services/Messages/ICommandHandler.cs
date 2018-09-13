using System.Threading.Tasks;

namespace MyStore.Services.Messages
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}