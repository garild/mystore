using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Carts;
using MyStore.Services.Carts.Commands;
using MyStore.Services.Carts.Queries;
using MyStore.Services.Messages;

namespace MyStore.Web.Controllers
{
    [ApiController]
    public class CartController : BaseController
    {
        public CartController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCart query)
            => Single(await QueryAsync(query));

        [HttpPost("items")]
        public async Task<IActionResult> Post(AddItemToCart command)
        {
            await SendAsync(command);

            return NoContent();
        }
    }
}