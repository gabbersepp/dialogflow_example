using System;
using Newtonsoft.Json;
using RestSharp;

namespace WebApplication1.Bot
{
    public class BotProvider
    {
        private string baseUrl = "https://api.api.ai/v1/query?v=20150910";
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
                Lang = "de",
                Query = text,
                SessionId = "alisdalsdkjdjas"
            };
            var client = new RestClient(baseUrl);
            var request = new RestRequest("", Method.POST);
            request.AddHeader("Authorization", "Bearer " + accessToken);
            request.AddBody(JsonConvert.SerializeObject(obj));
            var response = client.Execute(request);
            var content = response.Content;
        }
    }
}