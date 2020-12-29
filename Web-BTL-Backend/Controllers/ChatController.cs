using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Web_BTL_Backend.Models;

namespace Web_BTL_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private static ConcurrentBag<StreamWriter> clients;
        static ChatController()
        {
            clients = new ConcurrentBag<StreamWriter>();
        }

        public async Task PostAsync(Message m)
        {
            m.dt = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            await ChatCallbackMsg(m);
        }
        private async Task ChatCallbackMsg(Message m)
        {
            foreach (var client in clients)
            {
                try
                {
                    var data = string.Format("data:{0}|{1}|{2}\n\n", m.username, m.text, m.dt);
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
        public HttpResponseMessage Subscribe()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            // response.Headers.Add("Content-Type", "text/event-stream");
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
