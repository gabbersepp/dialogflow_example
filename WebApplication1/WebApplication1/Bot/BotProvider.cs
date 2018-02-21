using System;
using RestSharp;

namespace WebApplication1.Bot
{
    public class BotProvider
    {
        private string baseUrl = "https://api.api.ai/";
        private string accessToken = "";

        public BotProvider()
        {
            if (accessToken == "")
            {
                throw new Exception("Bitte Accesstoken in Klasse BotProvider bereitstellen. Aber nicht einchecken :)");
            }
        }

        public void SendQuery(string text)
        {
            var obj = new BotQuery
            {
                lang = "de",
                query = text,
                sessionId = "alisdalsdkjdjas"
            };
            var client = new RestClient(baseUrl);
            var request = new RestRequest("v1/query?v=20150910", Method.POST);
            request.AddHeader("Authorization", "Bearer " + accessToken);
            request.AddJsonBody(obj);
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");
            var response = client.Execute(request);
            var content = response.Content;
        }
    }
}