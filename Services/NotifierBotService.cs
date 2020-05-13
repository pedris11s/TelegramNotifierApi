using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBotNotifierApi.Persistence.Models;
using Newtonsoft.Json;

namespace TelegramBotNotifierApi.Services
{
    public interface INotifierBotService
    {
        Task<bool> SendMessage(string username, string message);
    }

    public class NotifierBotService : INotifierBotService
    {
        ITelegramBotClient _botClient;
        IUserService _userService;

        public NotifierBotService(IUserService userService)
        {
            string accessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
            _botClient = new TelegramBotClient(accessToken);
            
            var me = _botClient.GetMeAsync().Result;
            Console.WriteLine($"[INFO] BotStarted!! Id: {me.Id} Name: {me.FirstName}.");

            _botClient.OnMessage += OnMessageEvent;
            _botClient.StartReceiving();

            _userService = userService;
        }

        async void OnMessageEvent(object sender, MessageEventArgs e) 
        {
            Console.WriteLine($"[INFO] Received a text message in chat id: {e.Message.Chat.Id} name: {e.Message.Chat.FirstName} Text: {e.Message.Text}.");
            
            await _botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: "No pierda su tiempo...solo se decir esto, aqui el que habla soy yo, usted solo lee!!!"
            );

            // Console.WriteLine(JsonConvert.SerializeObject(e.Message));

            if(e.Message.Text.Equals("/start"))
            {
                if(_userService.GetUser(e.Message.From.Username) == null)
                {
                    _userService.Create(new User{
                        UserId = e.Message.From.Id,
                        Username = e.Message.From.Username,
                        FirstName = e.Message.From.FirstName
                    });
                }
            }
        }

        public async Task<bool> SendMessage(string username, string message) 
        {
            try
            {
                var user = _userService.GetUser(username);
                if(user == null)
                {
                    Console.WriteLine($"[INFO] Username: {username} don't exist!");
                    return false;
                }

                await _botClient.SendTextMessageAsync(
                    chatId: user.UserId,
                    text: message
                );
                
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