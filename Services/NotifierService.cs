using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBotNotifierApi.Persistence.Models;
using Newtonsoft.Json;

namespace TelegramBotNotifierApi.Services
{
    public interface INotifierService
    {
        Task<bool> SendMessageToUser(string username, string message);

        Task<bool> SendMessageToChannel(string channelName, string message);
    }

    public class NotifierService : INotifierService
    {
        ITelegramBotService _botService;
        IUserService _userService;
        IChannelService _channelService;

        public NotifierService(IUserService userService, IChannelService channelService, ITelegramBotService botService)
        {
            _userService = userService;
            _channelService = channelService;
            _botService = botService;
        }

        public async Task<bool> SendMessageToChannel(string channelName, string message) 
        {
            try
            {
                var channel = _channelService.GetChannel(channelName);
                if(channel != null)
                {
                    foreach (var user in channel.Users)
                    {
                        try
                        {
                            message = $"*Message via channel* #{channelName}\n\n" + message; 
                            var response = await SendMessageToUser(user.Username, message);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"[ERROR] Sending message Channel: {channelName} Username: {user.Username} Text: {message} ERROR: {ex.Message}");
                        }
                    }
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ERROR] Sending message Channel: {channelName} Text: {message} ERROR: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> SendMessageToUser(string username, string message) 
        {
            try
            {
                var user = _userService.GetUser(username);
                if(user == null)
                {
                    Console.WriteLine($"[INFO] Username: {username} don't exist!");
                    return false;
                }

                await _botService.SendMessage(user.UserId, message);

                Console.WriteLine($"[INFO] Sended message Username: {username} Text: {message} ");
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ERROR] Sending message Username: {username} Text: {message} ERROR: {ex.Message}");
                throw ex;
            }
        }
    }
}