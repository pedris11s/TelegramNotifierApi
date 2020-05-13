
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TelegramBotNotifierApi.Persistence.Models
{
    public class EnvironmentConfig
    {
        public string AccessToken { get; set; }  
        public string TestChatId { get; set; }  
        public string DbConectionString { get; set; }  
    }

    public class ApiRequest
    {
        [Required]
        public string Message { get; set; }  
        // public string UserId { get; set; }  
        public string Username { get; set; }  
    }
}
