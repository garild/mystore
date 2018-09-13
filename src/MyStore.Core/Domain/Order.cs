using System;
using System.Collections.Generic;

namespace MyStore.Core.Domain
{
    // Agregat
    public class Order
    {
        public AggregateId Id { get; set; }
        public AggregateId UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CreatedAt { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}