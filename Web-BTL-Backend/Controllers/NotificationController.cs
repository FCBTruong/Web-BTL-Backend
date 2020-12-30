using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace Web_BTL_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private IHubContext<SignalHub> _hub;
        public NotificationController(IHubContext<SignalHub> hub)
        {
            _hub = hub;
        }

        /// <summary>
        /// Send message to all
        /// </summary>
        /// <param name="message"></param>
        [HttpPost("{message}")]
        public void Post(string message)
        {
            Console.WriteLine("333");
            _hub.Clients.All.SendAsync("publicMessageMethodName", message);
        }

        /// <summary>
        /// Send message to specific client
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="message"></param>
        [HttpPost("{idUser}/{message}")]
        public void Post(string idUser, string message)
        {
            string connectionId = SignalHub.getConnectIdByUserId(idUser);
            _hub.Clients.Client(connectionId).SendAsync("privateMessageMethodName", (message));
        }
    }
}
