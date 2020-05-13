using TelegramBotNotifierApi.Persistence.Models;
using System;
using TelegramBotNotifierApi.Persistence.Repositories;
using Microsoft.Extensions.Options;

namespace TelegramBotNotifierApi.Services
{
    public interface IUserService
    {
        User GetUser(string username);
        void Create(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Create(User user)
        {
            try
            {
                _userRepository.Create(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ERROR] Creating user: {ex.Message}");
            }
        }

        public User GetUser(string username)
        {
            try
            {
                var user = _userRepository.GetUser(username);
                return user;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ERROR] GetUser: {ex.Message}");
                throw ex;
            }
        }
    }
}