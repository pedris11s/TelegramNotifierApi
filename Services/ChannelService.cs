using TelegramBotNotifierApi.Persistence.Models;
using System;
using TelegramBotNotifierApi.Persistence.Repositories;
using Microsoft.Extensions.Options;

namespace TelegramBotNotifierApi.Services
{
    public interface IChannelService
    {
    }

    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepository;

        public ChannelService(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public void Create(Channel channel)
        {
            try
            {
                _channelRepository.Create(channel);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[ERROR] Creating channel: {ex.Message}");
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