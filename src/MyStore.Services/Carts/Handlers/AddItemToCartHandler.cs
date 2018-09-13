using System;
using System.Threading.Tasks;
using MyStore.Core.Domain;
using MyStore.Core.Domain.Repositories;
using MyStore.Services.Carts.Commands;
using MyStore.Services.Messages;
using MyStore.Web.Services;

namespace MyStore.Services.Carts.Handlers
{
    public class AddItemToCartHandler : ICommandHandler<AddItemToCart>
    {
        private readonly ICartProvider _cartProvider;
        private readonly IProductRepository _productRepository;

        public AddItemToCartHandler(ICartProvider cartProvider,
            IProductRepository productRepository)
        {
            _cartProvider = cartProvider;
            _productRepository = productRepository;
        }
        
        public async Task HandleAsync(AddItemToCart command)
        {
            var product = await _productRepository.GetAsync(command.ProductId);
            if (product == null)
            {
                throw new ArgumentException($"Product with id: '{command.ProductId}' was not found.",
                    nameof(command.ProductId));
            }

            var cart = _cartProvider.Get(command.UserId) ?? new Cart(command.UserId);
            cart.AddItem(product, command.Quantity);
            _cartProvider.Set(cart);
            await Task.CompletedTask;
        }
    }
}