using Fundamentos.Redis.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceStack.Redis;

namespace Fundamentos.Redis.Controllers
{
    /// <summary>
    /// Controller utilizando [LISTAS] diretamente a classe redisclient, isso pode causar problemas com sistemas multi threads
    /// </summary>
    [ApiController]
    [Route("service-stack-redis-list")]
    public class RedisClientListController : ControllerBase
    {
        private static string _listId = $"atendente:aa1e106a-4059-4a02-911e-bbc44fd79d39";
        private readonly IRedisClient _client;
        public RedisClientListController(
            IOptions<RedisSettings> options)
        {
            _client = new RedisClient(options.Value.RedisConnection);
        }

        /// <summary>
        /// Obtendo todos os elementos da lista
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(string[]), 200)]
        public IActionResult Get()
        {
            var ids = _client.GetAllItemsFromList(_listId);
            return Ok(ids);
        }

        /// <summary>
        /// Inserindo elemento na lista
        /// </summary>
        /// <param name="atendimentoid">id do atendimento</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Post(string atendimentoid)
        {
            _client.AddItemToList(_listId, atendimentoid);
            return Ok(atendimentoid);
        }

        /// <summary>
        /// Removendo elemento da lista
        /// </summary>
        /// <param name="atendimentoid">id do atendimento</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(long), 200)]
        public IActionResult Delete(string atendimentoid)
        {
            var result = _client.RemoveItemFromList(_listId, atendimentoid);
            return Ok(result);
        }
    }
}