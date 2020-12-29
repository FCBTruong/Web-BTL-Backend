using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Web_BTL_Backend.Models.ClientSendForm;

namespace Web_BTL_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private static ConcurrentBag<StreamWriter> clients;
        static NotificationController()
        {
            clients = new ConcurrentBag<StreamWriter>();
        }

        public async Task PostAsync(NotificationForm m)
        {
            await NotificationCallbackMsg(m);
        }
        private async Task NotificationCallbackMsg(NotificationForm m)
        {
            foreach (var client in clients)
            {
                try
                {
                    var data = string.Format("data:{0}|{1}|{2}\n\n", m.idUser, m.contentNotification, m.test);
                    await client.WriteAsync(data);
                    await client.FlushAsync();
                    client.Dispose();
                }
                catch (Exception)
                {
                    StreamWriter ignore;
                    clients.TryTake(out ignore);
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage Subscribe(HttpRequestMessage request)
        {
            var response = request.CreateResponse();
            response.Content = new PushStreamContent((a, b, c) =>
            { OnStreamAvailable(a, b, c); }, "text/event-stream");
            return response;
        }

        private void OnStreamAvailable(Stream stream, HttpContent content,
            TransportContext context)
        {
            var client = new StreamWriter(stream);
            clients.Add(client);
        }
    }
}
