using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;

namespace Fundamentos.Redis.Controllers
{
    /// <summary>
    /// Controller utilizando CONJUNTOS [SET]
    /// </summary>
    [ApiController]
    [Route("service-stack-redis-set")]
    public class SetController : ControllerBase
    {
        private const string setone = "set_one";
        private const string settwo = "set_two";
        private readonly ILogger<RedisClientController> _logger;
        private readonly IRedisClientsManager _manager;
        public SetController(
            ILogger<RedisClientController> logger,
            IRedisClientsManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        /// <summary>
        /// Obtendo a interceção entre dois conjuntos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("intersection")]
        [ProducesResponseType(typeof(string[]), 200)]
        public IActionResult GetIntersection()
        {
            using (var client = _manager.GetClient())
            {
                var item = client.GetIntersectFromSets(setone, settwo);
                return Ok(item);
            }
        }

        /// <summary>
        /// Obtendo a diferença entre dois conjuntos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("difference")]
        [ProducesResponseType(typeof(string[]), 200)]
        public IActionResult GetDifference()
        {
            using (var client = _manager.GetClient())
            {
                var item = client.GetDifferencesFromSet(setone, settwo);
                return Ok(item);
            }
        }

        /// <summary>
        /// Inserindo elemento no primeiro conjunto
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("add-elements-set-one")]
        public IActionResult AddElementsSetOne(string elements)
        {
            using (var client = _manager.GetClient())
            {
                client.AddItemToSet(setone, elements);
                return Ok();
            }
        }

        /// <summary>
        /// Inserindo elemento no segundo conjunto
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("add-elements-set-two")]
        public IActionResult AddElementsSetTwo(string elements)
        {
            using (var client = _manager.GetClient())
            {
                client.AddItemToSet(settwo, elements);
                return Ok();
            }
        }

        /// <summary>
        /// Removendo elemento do primeiro conjunto
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove-elements-set-one")]
        public IActionResult RemoveElementSetOne(string element)
        {
            using (var client = _manager.GetClient())
            {
                client.RemoveItemFromSet(setone, element);
                return Ok();
            }
        }

        /// <summary>
        /// Removendo elemento do segundo conjunto
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove-elements-set-two")]
        public IActionResult RemoveElementSetTwo(string element)
        {
            using (var client = _manager.GetClient())
            {
                client.RemoveItemFromSet(settwo, element);
                return Ok();
            }
        }
    }
}