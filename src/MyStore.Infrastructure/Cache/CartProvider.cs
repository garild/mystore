using System;
using Microsoft.Extensions.Caching.Memory;
using MyStore.Core.Domain;

namespace MyStore.Web.Services
{
    public class CartProvider : ICartProvider
    {
        private readonly IMemoryCache _cache;

        public CartProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Cart Get(Guid userId)
            => _cache.Get<Cart>(GetKey(userId));

        public void Set(Cart cart)
            => _cache.Set(GetKey(cart.UserId), cart);
            
        private static string GetKey(Guid userId)
            => $"users:{userId}:cart";
    }
}