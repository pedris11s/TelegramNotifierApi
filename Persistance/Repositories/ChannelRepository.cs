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
        void Create(Channel channel);
        Channel GetChannel(string channelName);
    }

    public class ChannelRepository : IChannelRepository
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<Channel> _channels { get; set; }
        
        public ChannelRepository()
        {
            _client = new MongoClient(Environment.GetEnvironmentVariable("DB_CONECTION_STRING"));
            _database = _client.GetDatabase("NotifierBotDb");
            _channels = _database.GetCollection<Channel>("Channels");
        }

        public Channel GetChannel(string channelName)
        {
            return _channels.Find(u => u.ChannelName == channelName).FirstOrDefault();
        }

        public void Create(Channel channel)
        {
            try
            {
                if(GetChannel(channel.ChannelName) == null)
                {
                    _channels.InsertOne(channel);
                    Console.WriteLine($"[INFO] Channel: {channel.ChannelName} saved successfully!");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}