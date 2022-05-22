using Fundamentos.Redis.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;

namespace Fundamentos.Redis.Controllers
{
    /// <summary>
    /// Controller utilizando [STRINGS] a classe IRedisClientsManager: para criar e encerrar as conexões evitando problema com múltiplas threads
    /// </summary>
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

        /// <summary>
        /// Obtendo usuário pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string id)
        {
            using (var client = _manager.GetClient())
            {
                var user = client.Get<User>(id);
                return Ok(user);
            }
        }

        /// <summary>
        /// Inserindo usuário
        /// </summary>
        /// <param name="user">objeto usuário</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(User user)
        {
            using (var client = _manager.GetClient())
            {
                client.Add(user.Id.ToString(), user);
                return Ok(user);
            }
        }

        /// <summary>
        /// Atualizando usuário
        /// </summary>
        /// <param name="id">id do usuário</param>
        /// <param name="user">objeto usuário</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(string id, User user)
        {
            using (var client = _manager.GetClient())
            {
                client.Set(id, user);
                return Ok(user);
            }
        }

        /// <summary>
        /// Removendo o usuário pelo id
        /// </summary>
        /// <returns></returns>
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