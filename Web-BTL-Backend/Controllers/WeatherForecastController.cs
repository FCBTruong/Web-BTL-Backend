using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web_BTL_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("/Message/GetDataJson")]
        public ActionResult<IEnumerable<string>> GetMM()
        {
            var s = "ddddhjjjjjjjjjjjjjjjjjjjjjjjjjjjj";
            return Ok(s);
        }

        [HttpGet]
        [Route("/Message/GetData")]
        public ActionResult<IEnumerable<string>> GetVVVV()
        {
            String str = "[ { \"name\":\"John\", \"age\":30, \"cars\": [ { \"name\":\"Ford\", \"models\":[ \"Fiesta\", \"Focus\", \"Mustang\" ] }, { \"name\":\"BMW\", \"models\":[ \"320\", \"X3\" ] } ] }, { \"name\":\"Maria\", \"age\":25, \"cars\": [ { \"name\":\"Fiat\", \"models\":[ \"500\", \"Panda\" ] } ] }, { \"name\":\"David\", \"age\":40, \"cars\": [ { \"name\":\"Ford\", \"models\":[ \"Fiesta\", \"Focus\", \"Mustang\" ] }, { \"name\":\"BMW\", \"models\":[ \"320\", \"X3\", \"X5\" ] }, { \"name\":\"Fiat\", \"models\":[ \"500\", \"Panda\" ] } ] } ]";
            return Ok(str);
        }
    }
}
