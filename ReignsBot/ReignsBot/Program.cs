using System;
using Telegram.Bot;

namespace ReignsBot
{
    class Program
    {
        static void Main(string[] args)
        {
            TelegramBotClient ClientBot = new TelegramBotClient("540336212:AAFDxXrlVvJ6rjRdUap4t0OYfUHkSJD99kw");
            string webhookUrl = Configuration["Settings:webhookUrl"];
            int maxConnections = int.Parse (Configuration["Settings:MaxConnections"]);
            UpdateType[] allowedUpdate = { UpdateType.MessageUpdate };

            TelegramBotClient.setWebHook(webhookurls, maxConnections, allowedUpdate);
            services.Addscoped<ITelegramBotClient>(ClientBot => telegramclient);

        }
    }
}
