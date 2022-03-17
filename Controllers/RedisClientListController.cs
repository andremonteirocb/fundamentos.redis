using Fundamentos.Redis.Entities;
using Fundamentos.Redis.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;

namespace Fundamentos.Redis.Controllers
{
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

        [HttpGet]
        public IActionResult Get()
        {
            var ids = _client.GetAllItemsFromList(_listId);
            return Ok(ids);
        }

        [HttpPost]
        public IActionResult Post(string atendimentoid)
        {
            _client.AddItemToList(_listId, atendimentoid);
            return Ok(atendimentoid);
        }

        [HttpDelete]
        public IActionResult Delete(string atendimentoid)
        {
            var result = _client.RemoveItemFromList(_listId, atendimentoid);
            return Ok(result);
        }
    }
}