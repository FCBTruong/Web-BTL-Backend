using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web_BTL_Backend
{
    [Authorize]
    public class SignalHub : Hub
    {
        public static ConcurrentDictionary<string, string> clients = new ConcurrentDictionary<string, string>();
        public void GetDataFromClient(string userId, string connectionId)
        {
            Console.WriteLine("112");
            Clients.Client(connectionId).SendAsync("clientMethodName", $"Updated userid {userId}");
        }

        public override Task OnConnectedAsync()
        {
            var identity = Context.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            string idUser = claim[2].Value;
            clients.TryAdd(idUser, Context.ConnectionId);
            string outConnect;
            clients.TryGetValue(idUser, out outConnect);
            Console.WriteLine(outConnect);
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

        public static string getConnectIdByUserId(string userId)
        {
            string outConnect;
            clients.TryGetValue(userId, out outConnect);
            return outConnect;
        }
    }
}
