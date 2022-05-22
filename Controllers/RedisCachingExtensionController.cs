using Fundamentos.Redis.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Fundamentos.Redis.Controllers
{
    /// <summary>
    /// Controller utilizando [STRINGS] a biblioteca RedisCachingExtension: Microsoft.Extensions.Caching.Redis
    /// </summary>
    [ApiController]
    [Route("microsoft-extensions-caching-redis")]
    public class RedisCachingExtensionController : ControllerBase
    {
        private readonly ILogger<RedisCachingExtensionController> _logger;
        private readonly IDistributedCache _cache;
        public RedisCachingExtensionController(
            ILogger<RedisCachingExtensionController> logger,
            IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        /// <summary>
        ///  Obtendo usuário pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var json = await _cache.GetStringAsync(id);
            if (json == null) return NotFound();

            return Ok(json.toUser());
        }

        /// <summary>
        /// Inserindo usuário
        /// </summary>
        /// <param name="user">objeto usuário</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            await _cache.SetStringAsync(user.Id.ToString(), user.toJson());
            return Ok(user);
        }

        /// <summary>
        /// Atualizando usuário
        /// </summary>
        /// <param name="id">id do usuário</param>
        /// <param name="user">objeto usuário</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put(string id, User user)
        {
            await _cache.SetStringAsync(id, user.toJson());
            return Ok(user);
        }

        /// <summary>
        /// Removendo o usuário pelo id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _cache.RemoveAsync(id);
            return Ok();
        }
    }
}