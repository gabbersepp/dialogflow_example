using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bot;

namespace WebApplication1.Controllers
{
    public class ValuesController : Controller
    {
        [HttpPost]
        [Route("api/bot/sendQuery")]
        public BotResponse SendQuery([FromBody] BotControllerRequest text)
        {
            var provider = new BotProvider();
            var response = provider.SendQuery(text.Text, text.SessionId);
            if (!response.Result.ActionIncomplete && provider.ProcessResponse(response) != null)
            {
                if (provider.ValidateResponse(response))
                {
                    return new BotResponse
                    {
                        Result = new BotResponseResult
                        {
                            Fulfillment = new BotResponseFulfillment {Speech = provider.ProcessResponse(response), Dialog = provider.GetBasicDialog(response)}
                        }
                    };
                }
                else
                {
                    return provider.SendQuery(text.Text, text.SessionId, provider.GetValidContexts(response));
                }
            }

            return response;
        }
    }
}