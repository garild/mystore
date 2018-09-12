using System;
using System.Collections.Generic;

namespace MyStore.Web.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CreatedAt { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}