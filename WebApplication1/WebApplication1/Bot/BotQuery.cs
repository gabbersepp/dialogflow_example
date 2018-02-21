using System.Collections.Generic;

namespace WebApplication1.Bot
{
    public class BotQuery
    {
       
        public string lang;
        public string sessionId;
        public List<BotResponseContext> contexts;
        public bool resetContexts;
        public dynamic @event;
    }

    public class BotQueryQuery : BotQuery
    {
        public string query;
    }
}