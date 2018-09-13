using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Core.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(AggregateId id);
        Task<IEnumerable<Product>> BrowseAsync(string name = "");
        Task AddAsync(Product product);
    }
}