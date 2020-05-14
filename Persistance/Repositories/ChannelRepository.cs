using System.Collections.Generic;
using TelegramBotNotifierApi.Persistence.Models;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;


namespace TelegramBotNotifierApi.Persistence.Repositories
{
    public interface IChannelRepository
    {
        Channel Create(Channel channel);
        Channel GetChannel(string channelName);
        Channel UpdateUsers(string channelId, List<User> users);
    }

    public class ChannelRepository : IChannelRepository
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<Channel> _channels { get; set; }
        
        public IUserRepository _userRepository;
        
        public ChannelRepository(IUserRepository userRepository)
        {
            _client = new MongoClient(Environment.GetEnvironmentVariable("DB_CONECTION_STRING"));
            _database = _client.GetDatabase("NotifierBotDb");
            _channels = _database.GetCollection<Channel>("Channels");

            _userRepository = userRepository;
        }

        public Channel GetChannel(string channelName)
        {
            return _channels.Find(u => u.ChannelName == channelName).FirstOrDefault();
        }

        public Channel GetChannelById(string channelId)
        {
            return _channels.Find(u => u.Id == channelId).FirstOrDefault();
        }

        public Channel Create(Channel channel)
        {
            try
            {
                if(GetChannel(channel.ChannelName) == null)
                {
                    if(channel.Users == null)
                        channel.Users = new List<User>();
                    else
                        channel.Users = CompleteUserData(channel.Users);

                    _channels.InsertOne(channel);
                    Console.WriteLine($"[INFO] Channel: {channel.ChannelName} saved successfully!");
                    return channel;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return null;
        }

        private List<User> CompleteUserData(List<User> users)
        {
            var usersInDb = new List<User>();
            foreach (var item in users)
            {
                var user = _userRepository.GetUser(item.Username);
                if(user != null)
                {
                    usersInDb.Add(user);
                }
            }
            return usersInDb;
        }

        public Channel UpdateUsers(string channelId, List<User> users)
        {
            try
            {
                var channel = GetChannelById(channelId); 
                if(channel != null)
                {
                    var usersInDb = CompleteUserData(users);

                    _channels.UpdateOne(p => p.Id == channelId, Builders<Channel>.Update.Set(p => p.Users, usersInDb));
                    
                    Console.WriteLine($"[INFO] Channel: {channel.ChannelName} UPDATED successfully!");
                    return channel;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}