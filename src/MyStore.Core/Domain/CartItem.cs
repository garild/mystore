using System;

namespace MyStore.Core.Domain
{
    public class CartItem
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalAmount => Quantity * UnitPrice;

        public CartItem(Product product, int quantity)
        {
            IncreaseQuantity(quantity);
            ProductId = product.Id;
            ProductName = product.Name;
            UnitPrice = product.Price;
        }

        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than 0.",
                    nameof(quantity));
            }

            Quantity += quantity;
        }
    }
}