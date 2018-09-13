using System.Threading.Tasks;

namespace MyStore.Services.Messages
{
    public interface IDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}