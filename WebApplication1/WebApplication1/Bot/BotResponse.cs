using System;
using System.Collections.Generic;

namespace WebApplication1.Bot
{
    public class BotResponse
    {
        public string Id;
        public DateTimeOffset Timestamp;
        public BotResponseResult Result;
        public string SessionId;
    }

    public class BotResponseResult
    {
        public string Action;
        public string ResolvedQuery;
        public string Source;
        public bool ActionIncomplete;
        public Dictionary<string, string> Parameters;
        public List<BotResponseContext> Contexts;
        public BotResponseFulfillment Fulfillment;
        public BotResponseMetadata Metadata;
    }

    public class BotResponseStatus
    {
        public int Code;
        public string ErrorType;
    }

    public class BotResponseContext
    {
        public string name;
        public Dictionary<string, string> parameters = new Dictionary<string, string>();
        public int lifespan = 5;

        public void AddParameter(string name, string value)
        {
            parameters[name] = value;
        }
    }

    public class BotResponseFulfillment
    {
        public string Speech;
        public Dialog Dialog;
    }

    public class BotResponseMetadata
    {
        public string IntentName;
    }
}