using System.Linq;
using System.Threading.Tasks;
using MyStore.Core.Domain;
using MyStore.Services.Carts.Dto;
using MyStore.Services.Carts.Queries;
using MyStore.Services.Messages;
using MyStore.Web.Services;

namespace MyStore.Services.Carts.Handlers
{
    public class GetCartHandler : IQueryHandler<GetCart, CartDto>
    {
        private readonly ICartProvider _cartProvider;

        public GetCartHandler(ICartProvider cartProvider)
        {
            _cartProvider = cartProvider;
        }
        
        public async Task<CartDto> HandleAsync(GetCart query)
        {
            var cart = _cartProvider.Get(query.UserId);

            return await Task.FromResult(cart == null ? null : Map(cart));
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