using System.Collections.Generic;

namespace WebApplication1.Bot
{
    public interface IBotIntentHandler
    {
        string GetMessage(BotResponse response);
        bool IsValid(BotResponse response);
        List<BotResponseContext> GetValidContextsFromCompleteIntent(BotResponse response);
        Dialog GetDialog(BotResponse response);
    }
}