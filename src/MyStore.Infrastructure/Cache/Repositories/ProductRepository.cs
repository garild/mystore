using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStore.Core.Domain;
using MyStore.Core.Domain.Repositories;

namespace MyStore.Infrastructure.Cache.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> _products = new List<Product>
        {
            new Product(Guid.NewGuid(), "Samsung S8", Guid.NewGuid(), 3000),
            new Product(Guid.NewGuid(), "IPhone", Guid.NewGuid(), 5000),
            new Product(Guid.NewGuid(), "Xiaomi Mi6", Guid.NewGuid(), 2000)
        };

        public async Task<Product> GetAsync(AggregateId id)
            => await Task.FromResult(_products.SingleOrDefault(p => p.Id == id));

        public async Task<IEnumerable<Product>> BrowseAsync(string name = "")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return _products;
            }

            return await Task.FromResult(_products.Where(p => p.Name.Contains(name)));
        }

        public async Task AddAsync(Product product)
        {
            _products.Add(product);
            await Task.CompletedTask;
        }
    }
}