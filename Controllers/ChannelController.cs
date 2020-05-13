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

        public ChannelController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpPost]
        [Route("/[controller]/create")]
        public ActionResult<Channel> Create(Channel input)
        {
            try
            {
                var response = _channelService.Create(input);
                if(response != null)
                    return Ok(response);
                
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("/[controller]/update/{channelId}")]
        public IActionResult Update(string channelId, List<User> users)
        {
            try
            {
                var response = _channelService.UpdateUsers(channelId, users);
                if(response != null)
                    return Ok(response);
                
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        
    }
}
