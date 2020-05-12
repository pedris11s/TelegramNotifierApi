using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelegramBotNotifierApi.Services;
using TelegramBotNotifierApi.Models;


namespace TelegramBotNotifierApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifierController : ControllerBase
    {
        public INotifierBotService _notifierService;

        public NotifierController(INotifierBotService notifierService)
        {
            _notifierService = notifierService;
        }
        
        [HttpPost]
        [Route("/[controller]/send_message")]
        public void SendMessage([FromBody]ApiRequest request)
        {
            _notifierService.SendMessage(request.Message).GetAwaiter().GetResult();
        }
    }
}
