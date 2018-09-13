using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyStore.Infrastructure.EF;

namespace MyStore.Infrastructure.Dapper
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IOptions<SqlOptions> _sqlOptions;

        public DatabaseManager(IOptions<SqlOptions> sqlOptions)
        {
            _sqlOptions = sqlOptions;
        }
        
        public async Task ExecuteAsync(Func<SqlConnection, Task> connection)
        {
            using (var sqlConnection = new SqlConnection(_sqlOptions.Value.ConnectionString))
            {
                await connection(sqlConnection);
            }
        }

        public async Task<T> QueryAsync<T>(Func<SqlConnection, Task<T>> connection)
        {
            using (var sqlConnection = new SqlConnection(_sqlOptions.Value.ConnectionString))
            {
                return await connection(sqlConnection);
            }
        }
    }
}