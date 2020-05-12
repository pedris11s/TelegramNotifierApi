using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TelegramBotNotifierApi.Services;
using TelegramBotNotifierApi.Models;
using Newtonsoft.Json;

namespace TelegramBotNotifierApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = JsonConvert.DeserializeObject<EnvironmentConfig>(File.ReadAllText("key.json"));
            
            Environment.SetEnvironmentVariable("ACCESS_TOKEN", config.AccessToken);
            Environment.SetEnvironmentVariable("TEST_CHAT_ID", config.TestChatId);

            var botService = new NotifierBotService();
            
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
