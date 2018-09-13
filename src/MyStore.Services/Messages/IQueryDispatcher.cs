using System.Threading.Tasks;

namespace MyStore.Services.Messages
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}