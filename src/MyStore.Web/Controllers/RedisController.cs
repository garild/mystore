using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace MyStore.Web.Controllers
{
    [Route("redis")]
    public class RedisController : Controller
    {
        private readonly IDistributedCache _cache;

        public RedisController(IDistributedCache cache)
        {
            _cache = cache;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(string key, string value)
        {
            await _cache.SetStringAsync(key, value);
            var result = await _cache.GetStringAsync(key);

            return Ok(result);
        }
    }
}