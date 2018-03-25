﻿using System;
using System.Threading.Tasks;
using Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using ReignsBot.classes;
using System.Collections.Generic;

namespace ReignsBot
{
    class Startup
    {
        static TelegramBotClient ClientBot = new TelegramBotClient("540336212:AAFDxXrlVvJ6rjRdUap4t0OYfUHkSJD99kw");
        static List<MenuItems> menuItems = new List<MenuItems>();
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

            Console.WriteLine("Starting the bot");
            ClientBot.StartReceiving();
            Console.WriteLine("Reciving: " + ClientBot.IsReceiving);

            Console.Write("Init MenutItems: ");
            menuItems.Add(new MenuItems("/start", "Start", "Start", "Funziona"));
            menuItems.Add(new MenuItems("/startrandom", "Start Random", "Start your adventure in a random mode", "Funziona"));
            menuItems.Add(new MenuItems("/startstory", "Start Story", "Start your adventure in your reign", "Funziona"));
            menuItems.Add(new MenuItems("/donate", "DONATION REQUEST", "Support our project", "FUCK YEAH"));
            menuItems.Add(new MenuItems("/stats", "Statistics", "Watch career stats of your reign", "Spread alto"));
            menuItems.Add(new MenuItems("/Button", "Test comand", "Test inline buttons", "Aspe"));
            menuItems.Add(new MenuItems("/help", "Help", "Show help page", MenuItems.CunstructHelpPage(menuItems)));
            Console.WriteLine("Complete!");

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

            Console.WriteLine("Bot init finshed");
            Console.WriteLine("!!!!!AT ANY TIME, TYPE ENTER KEY TO STOP THE BOT!!!!!");

            //Da utilizzare qualcosa di meglio come una command line
            Console.ReadLine();

            ClientBot.StopReceiving();
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
                    ClientBot.SendTextMessageAsync(e.Message.Chat.Id, item.Output);
                    return; //If one command is triggered, don't search for other commands to trigger!
                }
            }
            ClientBot.SendTextMessageAsync(e.Message.Chat.Id, "Non riesco a capirti zio, prova a scrivere '/' per vedere se esiste un comando simile");

        }
    }
}