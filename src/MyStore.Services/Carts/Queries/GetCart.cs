using System;
using MyStore.Services.Carts.Dto;
using MyStore.Services.Messages;

namespace MyStore.Services.Carts.Queries
{
    public class GetCart : IQuery<CartDto>
    {
        public Guid UserId { get; set; }
    }
}