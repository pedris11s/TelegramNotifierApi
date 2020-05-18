using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

using Telegram.Bot;

using TelegramBotNotifierApi.Services;
using TelegramBotNotifierApi.Persistence.Models;
using TelegramBotNotifierApi.Persistence.Repositories;


namespace TelegramBotNotifierApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowCredentials();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TelegramBotNotifierApi", Version = "v1", Contact = new OpenApiContact{
                    Name = "PeterDev",
                    Url = new Uri("http://peterdev.me")
                }});
            });

            Console.WriteLine("ACCES_TOKEN = " + Environment.GetEnvironmentVariable("ACCESS_TOKEN"));

            services.AddSingleton<ITelegramBotClient>(provider => {
                return new TelegramBotClient(Environment.GetEnvironmentVariable("ACCESS_TOKEN"));
            });

            services.AddSingleton<ITelegramBotService, TelegramBotService>();
            services.AddSingleton<INotifierService, NotifierService>();

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IChannelRepository, ChannelRepository>();
            services.AddSingleton<IChannelService, ChannelService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var config = JsonConvert.DeserializeObject<EnvironmentConfig>(File.ReadAllText("key.json"));
                Environment.SetEnvironmentVariable("ACCESS_TOKEN", config.AccessToken);
                Environment.SetEnvironmentVariable("TEST_CHAT_ID", config.TestChatId);
                Environment.SetEnvironmentVariable("DB_CONECTION_STRING", config.DbConectionString);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowAll");

            app.UseSwagger();
            app.UseSwaggerUI(c=>    
            {    
                c.SwaggerEndpoint("/swagger/v1/swagger.json","TelegramBotNotifierApi");    
            }); 

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
