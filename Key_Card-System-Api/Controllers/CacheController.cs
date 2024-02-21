using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Key_Card_System_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly IDistributedCache _cache;

        public CacheController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetCacheValue(string key)
        {
            var cachedValue = await _cache.GetAsync(key);
            if (cachedValue != null)
            {
                var cachedString = Encoding.UTF8.GetString(cachedValue);
                return Ok($"Cached value for key '{key}': {cachedString}");
            }
            else
            {
                return NotFound($"No cached value found for key '{key}'");
            }
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> SetCacheValue(string key, [FromBody] string value)
        {
            var encodedValue = Encoding.UTF8.GetBytes(value);
            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromDays(5));
            await _cache.SetAsync(key, encodedValue, options);
            return Ok($"Value for key '{key}' set in cache");
        }

    }
}
