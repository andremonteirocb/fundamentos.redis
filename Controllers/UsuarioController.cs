using Fundamentos.Redis.Entities;
using Fundamentos.Redis.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Fundamentos.Redis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IDistributedCache _cache;
        public UsuarioController(
            ILogger<UsuarioController> logger,
            IDistributedCache cache,
            IOptions<RedisSettings> settings)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var json = await _cache.GetStringAsync(id.ToString());
            if (json == null) return NotFound();

            return Ok(json.toUser());
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            await _cache.SetStringAsync(user.Id.ToString(), user.toJson());
            return Ok(user);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _cache.RemoveAsync(id.ToString());
            return Ok();
        }
    }
}