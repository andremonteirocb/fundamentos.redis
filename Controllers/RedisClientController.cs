using Fundamentos.Redis.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;

namespace Fundamentos.Redis.Controllers
{
    [ApiController]
    [Route("service-stack-redis")]
    public class RedisClientController : ControllerBase
    {
        private readonly ILogger<RedisClientController> _logger;
        private readonly IRedisClientsManager _manager;
        public RedisClientController(
            ILogger<RedisClientController> logger,
            IRedisClientsManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        public IActionResult Get(string id)
        {
            using (var client = _manager.GetClient())
            {
                var user = client.Get<User>(id);
                return Ok(user);
            }
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            using (var client = _manager.GetClient())
            {
                client.Add(user.Id.ToString(), user);
                return Ok(user);
            }
        }

        [HttpPut]
        public IActionResult Put(string id, User user)
        {
            using (var client = _manager.GetClient())
            {
                client.Set(id, user);
                return Ok(user);
            }
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            using (var client = _manager.GetClient())
            {
                client.Remove(id);
                return Ok();
            }
        }
    }
}