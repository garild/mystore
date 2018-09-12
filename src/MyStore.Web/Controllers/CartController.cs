using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Carts;

namespace MyStore.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cart = await _cartService.GetAsync(Guid.Empty);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task<IActionResult> Post(AddItemToCart request)
        {
            await _cartService.AddItemAsync(Guid.Empty, request.ProductId, request.Quantity);

            return NoContent();
        }

        public class AddItemToCart
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}