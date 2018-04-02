using System;
using System.Threading.Tasks;
using Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using ReignsBot.classes;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace ReignsBot
{
    class Startup
    {
        static TelegramBotClient ClientBot = new TelegramBotClient("540336212:AAFDxXrlVvJ6rjRdUap4t0OYfUHkSJD99kw");
        static List<MenuItems> menuItems = new List<MenuItems>();
        static ReplyKeyboardMarkup markup = new ReplyKeyboardMarkup();

        //static Chat ChatBot;
        //static User UserBot;
        //static Update UpdateBot;
        //static WebhookInfo WebHookInfoBot;
        //static Task<User> TaskUserBot;
        //static Task<WebhookInfo> TaskWebHookInfoBot;
        //static Task<Chat> TaskChatBot;




        static void Main(string[] args)
        {
            ClientBot.OnMessage += ClientBot_OnMessage;
            ClientBot.OnMessageEdited += ClientBot_OnMessage;
            ClientBot.OnCallbackQuery += ClientBot_OnCallbackQuery1;
            ClientBot.OnInlineQuery += ClientBot_OnInlineQuery;
            ClientBot.OnInlineResultChosen += ClientBot_OnInlineResultChosen;
            ClientBot.OnReceiveError += ClientBot_OnReceiveError;


            Console.WriteLine("Starting the bot");
            ClientBot.StartReceiving();
            Console.WriteLine("Reciving: " + ClientBot.IsReceiving);

            #region Menu init

            Console.Write("Init MenutItems: ");
            menuItems.Add(new MenuItems("/start", "Start", "Start", "Ciao,benvenuto nel magico mondo di Reigns, dimostra a tutti le tue doti da sovrano!"));
            menuItems.Add(new MenuItems("/startrandom", "Start Random", "Start your adventure in a random mode", "Sto genetando le domande da porti"));
            menuItems.Add(new MenuItems("/startstory", "Start Story", "Start your adventure in your reign", "Che la storia abbia inizio"));
            menuItems.Add(new MenuItems("/donate", "DONATION REQUEST", "Support our project", "Supportaci donando a paypal.me/Frikyfriks"));
            menuItems.Add(new MenuItems("/stats", "Statistics", "Watch career stats of your reign", "Ecco le tue statistiche:"));
            menuItems.Add(new MenuItems("/rules", "Game Rules", "Learn how to play", "Ogni anno arriva una nuova \nimportante e apparentemente casuale richiesta dal tuo imprevedibile regno, mentre dovrai fare tutto il possibile per mantenere l'equilibrio tra le seguenti classi:\n• Chiesa ⛪️ \n• Popolo 👨‍👩‍👧‍👦\n• Esercito ⚔️\n• Denaro 💰\nDecisioni attente e pianificazione ti porteranno a regnare a lungo, ma cause impreviste, eventi a sorpresa e scarsa fortuna possono far cadere anche il più radicato dei monarchi.Rimani a capo del tuo regno per più tempo possibile, crea alleanze, fatti dei nemici e trova nuovi modi per morire mentre la tua dinastia si fa strada negli anni."));
            menuItems.Add(new MenuItems("/button", "Test comand", "Test inline buttons", InlineKeyboardButton.WithCallbackData("Testo"), "Testo"));
            menuItems.Add(new MenuItems("/help", "Help", "Show help page", MenuItems.CunstructHelpPage(menuItems)));
            Console.WriteLine("Complete!");

            #endregion

            #region Commento

            /*
             *  ClientBot.SetWebhookAsync("");
             *  TaskWebHookInfoBot = ClientBot.GetWebhookInfoAsync();
             *
             *  Console.WriteLine("Web Hook Info: \n" +
             *      "\t" + "Id:" + TaskWebHookInfoBot.Id + "\n" +
             *      "\t" + "Status: " + TaskWebHookInfoBot.Status + "\n" +
             *      "\t" + "Creation Options: " + TaskWebHookInfoBot.CreationOptions + "\n" +
             *      "\t" + "Result: " + TaskWebHookInfoBot.Result + "\n"
             *      );  
             */

            #endregion

            Console.WriteLine("Bot init finshed");


            Console.WriteLine("-----------------------------------------------------\n" +
                              "!!!!!AT ANY TIME, TYPE ENTER KEY TO STOP THE BOT!!!!!\n" +
                              "-----------------------------------------------------\n");

            //Da utilizzare qualcosa di meglio come una command line
            Console.ReadLine();

            ClientBot.StopReceiving();
        }

        private static void ClientBot_OnReceiveError(object sender, Telegram.Bot.Args.ReceiveErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void ClientBot_OnInlineResultChosen(object sender, Telegram.Bot.Args.ChosenInlineResultEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void ClientBot_OnInlineQuery(object sender, Telegram.Bot.Args.InlineQueryEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void ClientBot_OnCallbackQuery1(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            ClientBot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Ecco");
        }

        private static void ClientBot_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            ClientBot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Ecco");
        }



        private static void ClientBot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Console.WriteLine("[" + DateTime.Now + "]" +
                " Chat message recived: " + e.Message.Text +
                "\t| From: " + e.Message.Chat.Username +
                " Id: " + e.Message.Chat.Id);

            foreach (MenuItems item in menuItems)
            {
                if (e.Message.Text == item.Trigger)
                {
                    Console.WriteLine("\tCommand triggered: " + item.CommandName + "\t| Trigger: " + item.Trigger);
                    switch (item.Output.Type)
                    {
                        case Output.OutType.IsString:
                            ClientBot.SendTextMessageAsync(e.Message.Chat.Id, item.TextOutput);
                            break;

                        case Output.OutType.IsInlineKeyboardButton:
                            ClientBot.SendTextMessageAsync(e.Message.Chat.Id, item.TextOutput,
                                Telegram.Bot.Types.Enums.ParseMode.Html, false, false, 0, new InlineKeyboardMarkup());
                            break;

                        default:
                            Console.WriteLine("Something went wrong :/");
                            break;
                    }
                    return; //If one command is triggered, don't search for other commands to trigger!
                }
            }              
           

            InlineKeyboardMarkup menuinizio = new InlineKeyboardMarkup(
            new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                    {
                        new InlineKeyboardCallbackButton("Random Mode 🎲","rand"),
                        new InlineKeyboardCallbackButton("Story Mode ⚜️","story"),
                    },

                new InlineKeyboardButton[]
                    {
                        new InlineKeyboardCallbackButton("Come si gioca? 🔎","rules"),
                    },
                new InlineKeyboardUrlButton[]
                    {
                        new InlineKeyboardUrlButton ("Donate 💰","https://www.paypal.me/FrikyFriks"),
                        new InlineKeyboardUrlButton ("Support  ⛑", "https://t.me/ReignsSupportBot"),     
                    }
                    });

            string Str = "Ciao! Benvenuto nel bot Reigns, che ti permetterà di gestire il tuo regno! 🏰 \nRiuscirai a rendere contenti tutti? Quanto sopravviverai ? ";
            ClientBot.SendTextMessageAsync(e.Message.Chat.Id, Str, replyMarkup:menuinizio);

            
            
            

        }
        
    }
}