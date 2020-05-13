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

        
    }
}