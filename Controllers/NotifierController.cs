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
        public INotifierBotService _notifierService;

        public NotifierController(INotifierBotService notifierService)
        {
            _notifierService = notifierService;
        }
        
        [HttpPost]
        [Route("/[controller]/sendMessage")]
        public IActionResult SendMessage([FromBody]ApiRequest request)
        {
            try
            {
                var response = _notifierService.SendMessage(request.Username, request.Message).GetAwaiter().GetResult();
                if(response)
                    return Ok();
                return NotFound("User don't exist");
            }
            catch(Exception ex)
            {
                // @FIXME change this response
                return BadRequest(ex.Message);
            }
            
        }
    }
}
