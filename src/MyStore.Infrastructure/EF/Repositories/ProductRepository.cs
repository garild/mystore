using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStore.Core.Domain;
using MyStore.Core.Domain.Repositories;

namespace MyStore.Infrastructure.EF.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyStoreContext _context;

        public ProductRepository(MyStoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetAsync(AggregateId id)
            => await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Product>> BrowseAsync(string name = "")
        {
            var products = _context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            return await products.ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                transaction.Commit();
            }
        }
    }
}