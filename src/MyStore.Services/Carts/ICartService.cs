using System;
using System.Threading.Tasks;
using MyStore.Services.Carts.Dto;

namespace MyStore.Services.Carts
{
    public interface ICartService
    {
        Task<CartDto> GetAsync(Guid userId);
        Task AddItemAsync(Guid userId, Guid productId, int quantity);
    }
}