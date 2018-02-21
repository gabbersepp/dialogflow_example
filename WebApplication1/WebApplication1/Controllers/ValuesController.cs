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
        public void SendQuery([FromBody]string text)
        {
            new BotProvider().SendQuery(text);
        }
    }
}
