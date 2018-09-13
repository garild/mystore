using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyStore.Core.Domain;

namespace MyStore.Infrastructure.EF
{
    public class MyStoreContext : DbContext
    {
        private readonly IOptions<SqlOptions> _sqlOptions;

        public MyStoreContext(IOptions<SqlOptions> sqlOptions)
        {
            _sqlOptions = sqlOptions;
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            if (_sqlOptions.Value.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase(_sqlOptions.Value.Database);
                
                return;
            }

            optionsBuilder.UseSqlServer(_sqlOptions.Value.ConnectionString, 
                o => o.MigrationsAssembly("MyStore.Web"));
        }
    }
}