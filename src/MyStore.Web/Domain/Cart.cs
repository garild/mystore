using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStore.Web.Domain
{
    public class Cart
    {
        public Guid UserId { get; set; }
        public IEnumerable<CartItem> Items { get; set; }
        public decimal TotalAmount => Items.Sum(i => i.TotalAmount);
    }
}