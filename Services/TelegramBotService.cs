using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
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
        IHelperService _helperService;

        public TelegramBotService(IUserService userService, ITelegramBotClient botClient, IHelperService helperService)
        {
            _botClient = botClient;
            
            var me = _botClient.GetMeAsync().Result;
            Console.WriteLine($"[INFO] BotStarted!! Id: {me.Id} Name: {me.FirstName}.");

            _botClient.OnMessage += OnMessageEvent;
            _botClient.StartReceiving();

            _userService = userService;
            _helperService = helperService;
        }

        public async Task SendMessage(ChatId chatId, string message)
        {
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: message,
                parseMode: ParseMode.Markdown
            );
        }

        async void OnMessageEvent(object sender, MessageEventArgs e) 
        {
            Console.WriteLine($"[INFO] Received a text message in chat id: {e.Message.Chat.Id} name: {e.Message.Chat.FirstName} Text: {e.Message.Text}.");
            
            if(e.Message.Text.ToLower().Contains("users"))
            {
                string msg = _helperService.GetUsersCommand(e.Message);
                if(msg != null)
                {
                    await SendMessage(e.Message.Chat, msg);
                }
            }

            if(e.Message.Text.Equals("/start"))
            {
                var admin = _helperService.StartCommand(e.Message);
                if(admin != null)
                {
                    string msg = "Bienvenido...ahora puede recibir notificaciones mediante nuestra API. Visite nuestro repositorio para mas info: https://github.com/pedris11s/TelegramNotifierApi" ;
                    await SendMessage(e.Message.Chat, msg);

                    msg = $"Nuevo usuario registrado!\n Id: {e.Message.Chat.Id}\n Username: @{e.Message.Chat.Username}\n FirstName: {e.Message.Chat.FirstName}\n";
                    await SendMessage(new Chat{
                        Id = admin.UserId,
                        Username = admin.Username
                    }, msg);
                }
            }
        }
    }
}