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
    public class UserController : ControllerBase
    {
        public IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/[controller]")]
        public ApiResponse Create(User input)
        {
            try
            {
                _userService.Create(input);
                return ApiResponses.Success(null);
            }
            catch(Exception ex)
            {
                return ApiResponses.InternalError(null, ex.Message);
            }
        }

    }
}
