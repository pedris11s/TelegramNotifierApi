
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

    public class MessageRequest
    {
        public string Message { get; set; }  
        public string ChannelName { get; set; }  
        public string Username { get; set; }  
    }

    public class ApiResponse
    {
        public int Code { get; set; }  
        public string Message { get; set; }  
        public object Data { get; set; }
    }

    public static class ApiResponses
    {
        public static ApiResponse Success(object data, string message = "OK")
        {
            return new ApiResponse{
                Code = 200,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse InternalError(object data, string message = "Internal Error")
        {
            return new ApiResponse{
                Code = 500,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse Conflict(object data, string message = "Conflict")
        {
            return new ApiResponse{
                Code = 409,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse NotFound(object data, string message = "Not Found")
        {
            return new ApiResponse{
                Code = 404,
                Message = message,
                Data = data
            };
        }
    } 
}
