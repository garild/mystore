using System.Collections.Generic;

namespace MyStore.Services.Carts.Dto
{
    public class CartDto
    {
        public IEnumerable<CartItemDto> Items { get; set; }
    }
}