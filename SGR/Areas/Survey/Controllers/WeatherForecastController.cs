﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SGI.Entities;
using Connector;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace SGR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHubContext<HubConnector, IHubConnector> _hub;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };        

        public WeatherForecastController(IHubContext<HubConnector, IHubConnector> hub)
        {
            _hub = hub;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();

            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            //await _hub.Clients.All.GetChatMessage("Hola cómo estás?");

            await _hub.Clients.All.GetMessage(data);


            return data;
        }
    }
}