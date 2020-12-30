using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
        [Authorize]
        [HttpPost("AdminNotify/{message}")]
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
        [Authorize]
        [HttpPost("AdminNotify/{idUser}/{message}")]
        public ActionResult Post(string idUser, string message)
        {
            string connectionId = SignalHub.getConnectIdByUserId(idUser);
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            if (claim[1].Value != "admin") return Unauthorized("Only admin can use this function!");

            if (connectionId == "")
            {
                return BadRequest("User Not Online!");
            }
            _hub.Clients.Client(connectionId).SendAsync("privateAdminNotify", (message));
            return Ok();
        }
    }
}
