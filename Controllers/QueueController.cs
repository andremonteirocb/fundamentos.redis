using Fundamentos.Redis.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;

namespace Fundamentos.Redis.Controllers
{
    /// <summary>
    /// Controller utilizando [FILAS]
    /// </summary>
    [ApiController]
    [Route("service-stack-redis-queue")]
    public class QueueController : ControllerBase
    {
        private const string queuename = "fila:confirma-email";
        private readonly ILogger<RedisClientController> _logger;
        private readonly IRedisClientsManager _manager;
        public QueueController(
            ILogger<RedisClientController> logger,
            IRedisClientsManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        /// <summary>
        /// Obtendo o primeiro elemento e removendo da fila
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Get()
        {
            using (var client = _manager.GetClient())
            {
                //var item = client.DequeueItemFromList(queuename); //recupera o último elemento da lista
                //var item = client.PopItemFromList(queuename); //recupera o último elemento da lista
                //var item = client.BlockingDequeueItemFromList(queuename, TimeSpan.FromSeconds(15)); //recupera o elemento da lista esperando um determinado tempo
                //var item = client.GetAndRemoveFirstElementFromListWithBlockWait(queuename, 10);  //recupera o elemento da lista esperando um determinado tempo
                var item = client.GetAndRemoveFirstElementFromList(queuename);
                return Ok(item);
            }
        }

        /// <summary>
        /// Inserindo um elemento ao final da fila
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(string email)
        {
            using (var client = _manager.GetClient())
            {
                client.AddElementRightPositionList(queuename, email);
                //client.PushItemToList(queuename, email); //adiciona o elemento a direita da lista
                return Ok();
            }
        }

        /// <summary>
        /// Removendo um elemento da fila
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            using (var client = _manager.GetClient())
            {
                client.Remove(queuename);
                return Ok();
            }
        }
    }
}