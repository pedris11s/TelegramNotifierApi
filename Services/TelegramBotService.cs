using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Args;
using TelegramBotNotifierApi.Persistence.Models;

using User = TelegramBotNotifierApi.Persistence.Models.User;

namespace TelegramBotNotifierApi.Services
{
    public interface ITelegramBotService
    {
        Task SendMessage(ChatId chatId, string message);
    }

    public class TelegramBotService : ITelegramBotService
    {
        ITelegramBotClient _botClient;
        IUserService _userService;

        public TelegramBotService(IUserService userService, ITelegramBotClient botClient)
        {
            _botClient = botClient;
            
            var me = _botClient.GetMeAsync().Result;
            Console.WriteLine($"[INFO] BotStarted!! Id: {me.Id} Name: {me.FirstName}.");

            _userService = userService;
        }

        public async Task SendMessage(ChatId chatId, string message)
        {
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: message
            );
        }

        // async void OnMessageEvent(object sender, MessageEventArgs e) 
        // {
        //     Console.WriteLine($"[INFO] Received a text message in chat id: {e.Message.Chat.Id} name: {e.Message.Chat.FirstName} Text: {e.Message.Text}.");
            
        //     // Console.WriteLine(JsonConvert.SerializeObject(e.Message));

        //     if(e.Message.Text.Equals("/start"))
        //     {
        //         if(_userService.GetUser(e.Message.From.Username) == null)
        //         {
        //             _userService.Create(new User{
        //                 UserId = e.Message.From.Id,
        //                 Username = e.Message.From.Username,
        //                 FirstName = e.Message.From.FirstName
        //             });
        //         }
        //         await SendMessage(e.Message.Chat,"Bienvenido...ahora puede recibir notificaciones de sus canales. Consulte la API https://notifier-bot-api.herokuapp.com/swagger");
        //     }
        // }
    }
}