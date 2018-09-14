using System.Threading.Tasks;
using DShop.Common.Consul;
using Microsoft.AspNetCore.Mvc;

namespace MyStore.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly ConsulHttpClient _consulHttpClient;

        public UsersController(ConsulHttpClient consulHttpClient)
        {
            _consulHttpClient = consulHttpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _consulHttpClient.GetAsync<object>("api/users");

            return Ok(users);
        }
    }
}