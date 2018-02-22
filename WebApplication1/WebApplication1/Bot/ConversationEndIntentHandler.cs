using System.Collections.Generic;

namespace WebApplication1.Bot
{
    public class ConversationEndIntentHandler : IBotIntentHandler
    {
        public string GetMessage(BotResponse response)
        {
            return null;
        }

        public bool IsValid(BotResponse response)
        {
            return true;
        }

        public List<BotResponseContext> GetValidContextsFromCompleteIntent(BotResponse response)
        {
            return null;
        }

        public Dialog GetDialog(BotResponse response)
        {
            return new Dialog().AddTextPanel("Haben wir Ihnen geholfen?", "text1").AddButton("Ja", "yes").AddButton("Nein", "no").AddTextPanel("Grund", "reasonp").AddTextInput("reason");
        }
    }
}