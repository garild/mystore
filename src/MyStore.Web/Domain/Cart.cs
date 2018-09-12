using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStore.Web.Domain
{
    public class Cart
    {
        private readonly ISet<CartItem> _items = new HashSet<CartItem>();
        
        public Guid UserId { get; set; }
        public IEnumerable<CartItem> Items => _items;
        public decimal TotalAmount => Items.Sum(i => i.TotalAmount);

        public void AddItem(Guid productId, int quantity)
        {
            var item = _items.SingleOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity += quantity;

                return;
            }

            _items.Add(new CartItem
            {
                ProductId = productId,
                Quantity = quantity
            });
        }
    }
}