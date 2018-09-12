using System;
using Microsoft.AspNetCore.Mvc;
using MyStore.Web.Domain;
using MyStore.Web.Services;

namespace MyStore.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartProvider _cartProvider;

        public CartController(ICartProvider cartProvider)
        {
            _cartProvider = cartProvider;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var cart = _cartProvider.Get(Guid.Empty);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost("items")]
        public IActionResult Post(AddItemToCart request)
        {
            var cart = _cartProvider.Get(Guid.Empty) ?? new Cart();
            cart.AddItem(request.ProductId, request.Quantity);
            _cartProvider.Set(cart);

            return NoContent();
        }

        public class AddItemToCart
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}