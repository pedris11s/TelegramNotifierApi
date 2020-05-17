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
    public class ChannelController : ControllerBase
    {
        public IChannelService _channelService;

        public ITelegramBotService _telegramBotService;

        public ChannelController(IChannelService channelService, ITelegramBotService telegramBotService)
        {
            _channelService = channelService;
            _telegramBotService = telegramBotService;
        }

        [HttpGet]
        [Route("/wakeup")]
        public ApiResponse WakeUp()
        {
            try
            {
                return ApiResponses.Success(null);
            }
            catch(Exception ex)
            {
                return ApiResponses.InternalError(null, ex.Message);
            }
        }

        [HttpGet]
        [Route("/[controller]/get/{channelName}")]
        public ApiResponse Create(string channelName)
        {
            try
            {
                var response = _channelService.GetChannel(channelName);
                if(response != null)
                    return ApiResponses.Success(response);
             
                return ApiResponses.NotFound(channelName);
            }
            catch(Exception ex)
            {
                return ApiResponses.InternalError(null, ex.Message);
            }
        }

        [HttpPost]
        [Route("/[controller]/add")]
        public ApiResponse Create(Channel input)
        {
            try
            {
                var response = _channelService.Create(input);
                if(response != null)
                    return ApiResponses.Success(response);
                
                return ApiResponses.Conflict(input.ChannelName, "Channel already exists!");
            }
            catch(Exception ex)
            {
                return ApiResponses.InternalError(null, ex.Message);
            }
        }

        [HttpPut]
        [Route("/[controller]/updateUsers/{channelId}")]
        public ApiResponse UpdateUsers(string channelId, List<User> users)
        {
            try
            {
                var response = _channelService.UpdateUsers(channelId, users);
                if(response != null)
                    return ApiResponses.Success(response);
                
                return ApiResponses.NotFound(channelId);
            }
            catch(Exception ex)
            {
                return ApiResponses.InternalError(null, ex.Message);
            }
        }
        
    }
}
