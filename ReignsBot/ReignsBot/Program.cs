using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ReignsBot
{
    class Program
    {
        static TelegramBotClient ClientBot = new TelegramBotClient("540336212:AAFDxXrlVvJ6rjRdUap4t0OYfUHkSJD99kw");
        //static Chat ChatBot;
        //static User UserBot;
        //static Update UpdateBot;
        //static WebhookInfo WebHookInfoBot;
        //static Task<User> TaskUserBot;
        //static Task<WebhookInfo> TaskWebHookInfoBot;
        //static Task<Chat> TaskChatBot;/


        static void Main(string[] args)
        {
            ClientBot.OnMessage += ClientBot_OnMessage;

            Console.WriteLine("Starting the bot");
            ClientBot.StartReceiving();
            Console.WriteLine("Reciving: " + ClientBot.IsReceiving);

            /*
            ClientBot.SetWebhookAsync("");
            TaskWebHookInfoBot = ClientBot.GetWebhookInfoAsync();

            Console.WriteLine("Web Hook Info: \n" +
                "\t" + "Id:" + TaskWebHookInfoBot.Id + "\n" +
                "\t" + "Status: " + TaskWebHookInfoBot.Status + "\n" +
                "\t" + "Creation Options: " + TaskWebHookInfoBot.CreationOptions + "\n" +
                "\t" + "Result: " + TaskWebHookInfoBot.Result + "\n"
                );  
            */

            Console.WriteLine("Bot init finshed");

            Console.WriteLine("!!!!AT ANY TIME, TYPE ENTER KEY TO STOP THE BOT!!!!!");

            Console.ReadLine();

            ClientBot.StopReceiving();
        }

        private static void ClientBot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Console.WriteLine("Mi sono triggerato");
            switch (e.Message.Text)
            {
                case "/start" :
                    ClientBot.SendTextMessageAsync(e.Message.Chat.Id, "Ciao! Benvenuto nel bot Reigns," + 
                        " che ti permetterà di gestire il tuo regno!  Riuscirai a rendere contenti tutti ? Quanto sopravviverai ?");
                    break;
            }
        }
    }
}
