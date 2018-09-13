using System;
using MyStore.Services.Messages;
using Newtonsoft.Json;

namespace MyStore.Services.Carts.Commands
{
    public class AddItemToCart : ICommand
    {
        public Guid UserId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public AddItemToCart(Guid userId, Guid productId, int quantity)
        {
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}