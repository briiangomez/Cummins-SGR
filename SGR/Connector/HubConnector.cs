using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connector
{
    public class HubConnector : Hub<IHubConnector>
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();            
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
