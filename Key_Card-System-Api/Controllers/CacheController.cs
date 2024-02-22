using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Key_Card_System_Api.Controllers
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public CacheController(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetValue(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();

            var cachedValue = await db.StringGetAsync(key);
            if (!cachedValue.IsNull)
            {
                return Ok($"Cached value for key '{key}': {cachedValue}");
            }
            else
            {
                return NotFound($"No cached value found for key '{key}'");
            }
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> SetValue(string key, [FromBody] string value)
        {
            var db = _connectionMultiplexer.GetDatabase();

            await db.StringSetAsync(key, value);

            return Ok($"Value for key '{key}' set in cache");
        }
    }
}
