using System.Collections.Generic;
using TelegramBotNotifierApi.Persistence.Models;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;


namespace TelegramBotNotifierApi.Persistence.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        void Create(User user);
        User GetUser(string username);
        User GetUser(int userId);
    }

    public class UserRepository : IUserRepository
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<User> _users { get; set; }
        
        public UserRepository()
        {
            _client = new MongoClient(Environment.GetEnvironmentVariable("DB_CONECTION_STRING"));
            _database = _client.GetDatabase("NotifierBotDb");
            _users = _database.GetCollection<User>("Users");
        }

        public List<User> GetAll()
        {
            return _users.Find(u => true).ToList();
        }

        public User GetUser(int userId)
        {
            return _users.Find(u => u.UserId == userId).FirstOrDefault();
        }

        public User GetUser(string username)
        {
            return _users.Find(u => u.Username == username).FirstOrDefault();
        }

        public void Create(User user)
        {
            try
            {
                if(GetUser(user.UserId) == null)
                {
                    _users.InsertOne(user);
                    Console.WriteLine($"[INFO] User: {user.FirstName} saved successfully!");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}