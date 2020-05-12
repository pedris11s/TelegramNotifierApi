
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TelegramBotNotifierApi.Models
{
    public class EnvironmentConfig
    {
        public string AccessToken { get; set; }  
        public string TestChatId { get; set; }  
    }

    public class ApiRequest
    {
        [Required]
        public string Message { get; set; }  

        public string ChatId { get; set; }  
    }
}
