using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBotNotifierApi.Services
{
    public interface INotifierBotService
    {
        Task SendMessage(string message);
    }

    public class NotifierBotService : INotifierBotService
    {
        ITelegramBotClient _botClient;

        public NotifierBotService()
        {
            string accessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
            _botClient = new TelegramBotClient(accessToken);
            
            var me = _botClient.GetMeAsync().Result;
            Console.WriteLine($"BotStarted!! Id: {me.Id} Name: {me.FirstName}.");

            _botClient.OnMessage += OnMessageEvent;
            _botClient.StartReceiving();
        }

        async void OnMessageEvent(object sender, MessageEventArgs e) 
        {
            Console.WriteLine($"Received a text message in chat id: {e.Message.Chat.Id} name: {e.Message.Chat.FirstName}.");
            await _botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text:   "You said:\n" + e.Message.Text
            );
        }

        public async Task SendMessage(string message) 
        {
            Console.WriteLine($"Sending message: {message}");
            
            await _botClient.SendTextMessageAsync(
                chatId: Environment.GetEnvironmentVariable("TEST_CHAT_ID"),
                text: message
            );
        }
    }
}