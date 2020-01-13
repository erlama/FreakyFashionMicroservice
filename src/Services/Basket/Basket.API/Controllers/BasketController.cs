using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public BasketController(ILogger<BasketController> logger, ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            var value = _database.StringGet("name");
            return value;
        }

        [HttpPost]
        public IActionResult Post(Basket basket)
        {
            _database.StringSet("name", basket.Value);

            return Created("", null);
        }

        public class Basket
        {
            public string Value { get; set; }
        }
    }
}
