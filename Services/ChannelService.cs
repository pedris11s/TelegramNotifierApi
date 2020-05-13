using TelegramBotNotifierApi.Persistence.Models;
using System;
using System.Collections.Generic;
using TelegramBotNotifierApi.Persistence.Repositories;
using Microsoft.Extensions.Options;

namespace TelegramBotNotifierApi.Services
{
    public interface IChannelService
    {
        Channel Create(string channelName);
        Channel GetChannel(string channelName);
    }

    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepository;

        public ChannelService(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public Channel Create(string channelName)
        {
            try
            {
                return _channelRepository.Create(channelName);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ERROR] Creating channel: {ex.Message}");
                return null;
            }
        }

        public Channel GetChannel(string channelName)
        {
            try
            {
                var channel = _channelRepository.GetChannel(channelName);
                return channel;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ERROR] GetChannel: {ex.Message}");
                throw ex;
            }
        }
    }
}