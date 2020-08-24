using SGR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connector
{
    public interface IHubConnector
    {
        Task GetMessage(WeatherForecast [] forecasts);

        Task GetChatMessage(string msg);
    }
}
