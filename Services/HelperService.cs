using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Args;
using TelegramBotNotifierApi.Persistence.Models;

using User = TelegramBotNotifierApi.Persistence.Models.User;

namespace TelegramBotNotifierApi.Services
{
    public interface IHelperService
    {
        User StartCommand(Message msg);
        string GetUsersCommand(Message msg);
    }

    public class HelperService : IHelperService
    {
        IUserService _userService;

        public HelperService(IUserService userService)
        {
            _userService = userService;
        }


        public User StartCommand(Message msg)
        {
            if(_userService.GetUser(msg.From.Id) == null)
            {
                _userService.Create(new User{
                    UserId = msg.From.Id,
                    Username = msg.From.Username,
                    FirstName = msg.From.FirstName
                });
                
                Console.WriteLine($"[INFO] Nuevo usuario registrado!\n Id: {msg.Chat.Id}\n Username: @{msg.Chat.Username}\n FirstName: {msg.Chat.FirstName}\n");

                var admin = _userService.GetUser("pedris11s");

                return admin;
            }
            return null;
        }   

        public string GetUsersCommand(Message msg)
        {
            if(msg.From.Username.Equals("pedris11s"))
            {
                var users = _userService.GetAll();
                
                string text = $"Usuarios registrados({users.Count}):\n\n";

                int cont = 1;
                users.ForEach(u => {
                    text += $" {cont++}) UserId: {u.UserId}\n Username: @{u.Username}\n FirstName: {u.FirstName}\n\n";
                });
                return text;
            }
            return null;
        }   
    }
}