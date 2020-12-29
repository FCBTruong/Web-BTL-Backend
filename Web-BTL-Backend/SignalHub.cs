using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Web_BTL_Backend
{
    public class SignalHub : Hub
    {
        public void GetDataFromClient(string userId, string connectionId)
        {
            Console.WriteLine("112");
            Clients.Client(connectionId).SendAsync("clientMethodName", $"Updated userid {userId}");
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("222");
            var connectionId = Context.ConnectionId;
            Clients.Client(connectionId).SendAsync("WelcomeMethodName", connectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("333");
            var connectionId = Context.ConnectionId;
            return base.OnDisconnectedAsync(exception);
        }
    }
}
