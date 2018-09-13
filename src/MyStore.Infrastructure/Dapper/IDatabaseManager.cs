using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MyStore.Infrastructure.Dapper
{
    public interface IDatabaseManager
    {
        Task ExecuteAsync(Func<SqlConnection, Task> connection);
        Task<T> QueryAsync<T>(Func<SqlConnection, Task<T>> connection);
    }
}