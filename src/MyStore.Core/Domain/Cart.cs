using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStore.Core.Domain
{
    //Agregat
    public class Cart
    {
        private readonly ISet<CartItem> _items = new HashSet<CartItem>();
        
        public Guid UserId { get; private set; }
        public IEnumerable<CartItem> Items => _items;
        public decimal TotalAmount => Items.Sum(i => i.TotalAmount);
        
        public Cart(Guid userId)
        {
            UserId = userId;
        }

        public void AddItem(Product product, int quantity)
        {
            var item = _items.SingleOrDefault(i => i.ProductId == product.Id);
            if (item != null)
            {
                item.IncreaseQuantity(quantity);

                return;
            }

            _items.Add(new CartItem(product, quantity));
        }
    }
}