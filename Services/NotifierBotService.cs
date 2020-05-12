using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBotNotifierApi.Services
{
    public class NotifierBotService
    {
        ITelegramBotClient _botClient;

        public NotifierBotService()
        {
            string accessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
            _botClient = new TelegramBotClient(accessToken);
            
            var me = _botClient.GetMeAsync().Result;
            Console.WriteLine($"BotStarted!! Id: {me.Id} Name: {me.FirstName}.");

            _botClient.OnMessage += OnMessage;
            _botClient.StartReceiving();
        }

        async void OnMessage(object sender, MessageEventArgs e) 
        {
            Console.WriteLine($"Received a text message in chat id: {e.Message.Chat.Id} name: {e.Message.Chat.FirstName}.");
            await _botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text:   "You said:\n" + e.Message.Text
            );
        }
    }
}