using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace WebApplication1.Bot
{
    public class BotProvider
    {
        private string baseUrl = "https://api.api.ai/";
        private string accessToken = "";
        private static Dictionary<string, IBotIntentHandler> handlers = new Dictionary<string, IBotIntentHandler>();

        public BotProvider()
        {
            if (accessToken == "")
            {
                throw new Exception("Bitte Accesstoken in Klasse BotProvider bereitstellen. Aber nicht einchecken :)");
            }
        }

        public BotResponse SendQuery(string text, string sessionId, List<BotResponseContext> contexte = null)
        {
            BotQuery obj;

            if (contexte == null)
            {
                obj = new BotQueryQuery
                {
                    lang = "de",
                    query = text,
                    sessionId = sessionId
                };
            }
            else
            {
                obj = new BotQuery
                {
                    lang = "de",
                    sessionId = sessionId,
                    contexts = null,
                    resetContexts = true,
                    @event = new { name = "testevent", data = contexte[0].parameters }
                };
            }


            if (contexte != null)
            {
                var client1 = new RestClient(baseUrl);
                var request1 = new RestRequest("v1/contexts?sessionId=" + sessionId, Method.DELETE);
                request1.AddHeader("Authorization", "Bearer " + accessToken);
                var response1 = client1.Execute(request1);
                var content1 = response1.Content;
            }
            var client = new RestClient(baseUrl);
            var request = new RestRequest("v1/query?v=20150910", Method.POST);
            request.AddHeader("Authorization", "Bearer " + accessToken);
            request.AddJsonBody(obj);
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");
            var response = client.Execute(request);
            var content = response.Content;

            var d = JsonStuff.Deserialize<BotResponse>(content);
            return d;
        }

        public Dialog GetBasicDialog(BotResponse response)
        {
            IBotIntentHandler handler;
            if (handlers.TryGetValue(response.Result.Metadata.IntentName, out handler))
            {
                return handler.GetDialog(response);
            }

            return null;
        }

        public string ProcessResponse(BotResponse response)
        {
            IBotIntentHandler handler;
            if (handlers.TryGetValue(response.Result.Metadata.IntentName, out handler))
            {
                return handler.GetMessage(response);
            }

            return null;
        }

        public bool ValidateResponse(BotResponse response)
        {
            IBotIntentHandler handler;
            if (handlers.TryGetValue(response.Result.Metadata.IntentName, out handler))
            {
                return handler.IsValid(response);
            }

            return false;
        }

        public List<BotResponseContext> GetValidContexts(BotResponse response)
        {
            IBotIntentHandler handler;
            if (handlers.TryGetValue(response.Result.Metadata.IntentName, out handler))
            {
                return handler.GetValidContextsFromCompleteIntent(response);
            }

            return null;
        }

        public static void RegisterIntentHandler(string action, IBotIntentHandler handler)
        {
            handlers[action] = handler;
        }
    }
}