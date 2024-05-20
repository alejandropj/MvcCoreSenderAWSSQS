using Amazon.SQS;
using Amazon.SQS.Model;
using MvcCoreSenderAWSSQS.Models;
using Newtonsoft.Json;
using System.Net;

namespace MvcCoreSenderAWSSQS.Services
{
    public class ServiceSQS
    {
        private string UrlQueue;
        private IAmazonSQS clientSQS;

        public ServiceSQS(IAmazonSQS client,
            IConfiguration configuration)
        {
            this.clientSQS = client;
            this.UrlQueue = configuration.GetValue<string>
                ("AWS:UrlSQS");
        }

        public async Task SendMessageAsync(Mensaje mensaje)
        {
            string json = JsonConvert.SerializeObject(mensaje);
            SendMessageRequest request =
                new SendMessageRequest(this.UrlQueue, json);
            request.MessageGroupId = "developers";
            request.MessageDeduplicationId = "developers" + mensaje.Email;
            SendMessageResponse response =
                await this.clientSQS.SendMessageAsync(request);
            HttpStatusCode statusCode = response.HttpStatusCode;
        }


    }
}
