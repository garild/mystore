using System;
using System.Linq;
using System.Threading.Tasks;
using MyStore.Core.Domain;
using MyStore.Core.Domain.Repositories;
using MyStore.Services.Carts.Dto;
using MyStore.Web.Services;

namespace MyStore.Services.Carts
{
    public class CartService : ICartService
    {
        private readonly ICartProvider _cartProvider;
        private readonly IProductRepository _productRepository;

        public CartService(ICartProvider cartProvider,
            IProductRepository productRepository)
        {
            _cartProvider = cartProvider;
            _productRepository = productRepository;
        }

        public async Task<CartDto> GetAsync(Guid userId)
        {
            var cart = _cartProvider.Get(userId);

            return await Task.FromResult(cart == null ? null : Map(cart));
        }

        public async Task AddItemAsync(Guid userId, Guid productId, int quantity)
        {
            var product = await _productRepository.GetAsync(productId);
            if (product == null)
            {
                throw new ArgumentException($"Product with id: '{productId}' was not found.",
                    nameof(productId));
            }

            var cart = _cartProvider.Get(userId) ?? new Cart(userId);
            cart.AddItem(product, quantity);
            _cartProvider.Set(cart);
            await Task.CompletedTask;
        }

        private static CartDto Map(Cart cart)
            => new CartDto
            {
                Items = cart.Items.Select(i =>
                    new CartItemDto
                    {
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        Quantity = i.Quantity,
                        TotalAmount = i.TotalAmount,
                        UnitPrice = i.UnitPrice
                    })
            };
    }
}