using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyStore.Core.Domain;
using MyStore.Core.Domain.Repositories;

namespace MyStore.Infrastructure.Dapper.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDatabaseManager _databaseManager;

        public ProductRepository(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public async Task<Product> GetAsync(AggregateId id)
            => await _databaseManager.QueryAsync(c =>
                c.QueryFirstAsync<Product>("SELECT * FROM Products WHERE Id = @id", new {id}));

        public async Task<IEnumerable<Product>> BrowseAsync(string name = "")
            => await _databaseManager.QueryAsync(c =>
                c.QueryAsync<Product>("SELECT * FROM Products"));

        public async Task AddAsync(Product product)
            => await _databaseManager.ExecuteAsync(c =>
                c.QueryAsync("INSERT INTO Products (id, name, categoryId, price) " +
                             "VALUES (@id, @name, @categoryId, @price)",
                    new {product.Id, product.Name, product.CategoryId, product.Price}));
    }
}