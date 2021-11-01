using Fundamentos.Redis.Entities;
using Fundamentos.Redis.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceStack.Redis;

namespace Fundamentos.Redis.Controllers
{
    [ApiController]
    [Route("service-stack-redis")]
    public class RedisClientController : ControllerBase
    {
        private readonly ILogger<RedisClientController> _logger;
        private readonly IRedisClient _client;
        public RedisClientController(
            ILogger<RedisClientController> logger,
            IOptions<RedisSettings> options)
        {
            _logger = logger;
            _client = new RedisClient(options.Value.RedisConnection);
        }

        [HttpGet]
        public IActionResult Get(string id)
        {
            var user = _client.Get<User>(id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            _client.Add(user.Id.ToString(), user);
            return Ok(user);
        }

        [HttpPut]
        public IActionResult Put(string id, User user)
        {
            _client.Set(id, user);
            return Ok(user);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            _client.Remove(id);
            return Ok();
        }
    }
}