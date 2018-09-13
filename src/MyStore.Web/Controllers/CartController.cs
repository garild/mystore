using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStore.Services.Carts;
using MyStore.Services.Carts.Commands;
using MyStore.Services.Carts.Queries;
using MyStore.Services.Messages;
using MyStore.Web.Framework;

namespace MyStore.Web.Controllers
{
    [ApiController]
    [Authorize]
    public class CartController : BaseController
    {
        public CartController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCart query)
            => Single(await QueryAsync(query.Bind(q => q.UserId, UserId)));

        [HttpPost("items")]
        public async Task<IActionResult> Post(AddItemToCart command)
        {
            await SendAsync(command.Bind(c => c.UserId, UserId));

            return NoContent();
        }
    }
}