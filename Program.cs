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

namespace TelegramBotNotifierApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string accessToken = File.ReadAllText("key");
            Environment.SetEnvironmentVariable("ACCESS_TOKEN", accessToken);

            var botService = new NotifierBotService();
            
            Thread.Sleep(int.MaxValue);
            // CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
