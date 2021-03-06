using System;
using System.Net.Http;
// using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelegramBotNotifierApi.Services;
using TelegramBotNotifierApi.Persistence.Models;
// using IHttpActionResult = System.Web.Http.IHttpActionResult;


namespace TelegramBotNotifierApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifierController : ControllerBase
    {
        public INotifierService _notifierService;

        public NotifierController(INotifierService notifierService)
        {
            _notifierService = notifierService;
        }
        
        [HttpPost]
        [Route("/notify/user")]
        public ApiResponse SendMessageUser([FromBody]MessageRequest request)
        {
            try
            {
                var response = _notifierService.SendMessageToUser(request.Username, request.Message).GetAwaiter().GetResult();
                if(response)
                    return ApiResponses.Success(null);
                
                return null;
            }
            catch(Exception ex)
            {
                return ApiResponses.InternalError(null, ex.Message);
            }
        }

        [HttpPost]
        [Route("/notify/channel")]
        public ApiResponse SendMessageChannel([FromBody]MessageRequest request)
        {
            try
            {
                var response = _notifierService.SendMessageToChannel(request.ChannelName, request.Message).GetAwaiter().GetResult();
                if(response)
                    return ApiResponses.Success(null);
                
                return null;
            }
            catch(Exception ex)
            {
                return ApiResponses.InternalError(null, ex.Message);
            }
            
        }
    }
}
