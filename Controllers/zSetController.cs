using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;

namespace Fundamentos.Redis.Controllers
{
    /// <summary>
    /// Controller utilizando lista ordenada [ZSET]
    /// </summary>
    [ApiController]
    [Route("service-stack-redis-zset")]
    public class zSetController : ControllerBase
    {
        private const string setId = "rank";
        private readonly ILogger<RedisClientController> _logger;
        private readonly IRedisClientsManager _manager;
        public zSetController(
            ILogger<RedisClientController> logger,
            IRedisClientsManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        /// <summary>
        /// Obtendo todos os elementos
        /// </summary>
        /// <param name="orderDesc">tipo de ordenação</param>
        /// <returns></returns>
        [HttpGet]
        [Route("obter-todas-pontuacoes")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GetAll(bool orderDesc = false)
        {
            using (var client = _manager.GetClient())
            {
                //var itens = orderDesc ? client.GetAllItemsFromSortedSetDesc(setId) : client.GetAllItemsFromSortedSet(setId);
                var itens = orderDesc ? client.GetRangeFromSortedSetDesc(setId, 0, -1) : client.GetRangeFromSortedSet(setId, 0, -1);
                return Ok(itens);
            }
        }

        /// <summary>
        /// Obtendo valor de uma determinada chave
        /// </summary>
        /// <param name="value">chave para busca</param>
        /// <returns></returns>
        [HttpGet]
        [Route("obter-pontuacao")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Get(string value)
        {
            using (var client = _manager.GetClient())
            {
                //var item = client.GetItemIndexInSortedSet(setId, value); //recupera o índice atual do elemento na lista ordenada
                //var item3 = client.GetAllWithScoresFromSortedSet(setId); //recupera todos os itens adicionados para chave
                //var item4 = client.GetRangeFromSortedSet(setId, 0, -1); //recupera todos ps itens dentro de um range (-1 até o último elemento)
                var item = client.GetItemScoreInSortedSet(setId, value); //recupera o valor adicionando na lista ordenada
                return Ok(item);
            }
        }

        /// <summary>
        /// Inserindo elemento e sua pontuação na lista ordenada
        /// </summary>
        /// <param name="value">chave</param>
        /// <param name="score">valor</param>
        /// <returns></returns>
        [HttpPost]
        [Route("inserir-pontuacao")]
        public IActionResult Post(string value, double score)
        {
            using (var client = _manager.GetClient())
            {
                client.AddItemToSortedSet(setId, value, score); //adiciona um elemento na lista ordenada
                return Ok();
            }
        }

        /// <summary>
        /// Removendo determinado item da lista ordenada
        /// </summary>
        /// <param name="value">chave para remoção</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletar")]
        public IActionResult Delete(string value)
        {
            using (var client = _manager.GetClient())
            {
                client.RemoveItemFromSortedSet(setId, value); //remove o elemento da lista ordenada
                return Ok();
            }
        }
    }
}