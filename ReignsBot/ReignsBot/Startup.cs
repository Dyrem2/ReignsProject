using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using ReignsBot.classes;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReignsBot
{
    class Reigns
    {
        public static ConsoleColor console_default_color = Console.ForegroundColor;

        static TelegramBotClient ClientBot = new TelegramBotClient("540336212:AAFDxXrlVvJ6rjRdUap4t0OYfUHkSJD99kw");
        static List<MenuItems> menuItems = new List<MenuItems>();
        static ReplyKeyboardMarkup markup = new ReplyKeyboardMarkup();

        static MySqlConnection sql_connection;
        static DataSet data_set = new DataSet();
        static MySqlDataAdapter data_adapter;



        static void Main(string[] args)
        {
            #region Triggers

            ClientBot.OnMessage += ClientBot_OnMessage;
            ClientBot.OnMessageEdited += ClientBot_OnMessage;
            ClientBot.OnCallbackQuery += ClientBot_OnCallbackQuery;
            ClientBot.OnInlineQuery += ClientBot_OnInlineQuery;
            ClientBot.OnInlineResultChosen += ClientBot_OnInlineResultChosen;
            ClientBot.OnReceiveError += ClientBot_OnReceiveError;

            #endregion

            Console.WriteLine("Starting the bot");                                      //Console output starting
            ClientBot.StartReceiving();

            sql_connection = ConnectToDb();

            Console.Write("Reciving: ");
            if (ClientBot.IsReceiving) ConsoleOutputs.Output(ConsoleOutputs.OutputType.True);
            else ConsoleOutputs.Output(ConsoleOutputs.OutputType.False);

            //Get data from DB
            data_adapter = new MySqlDataAdapter("SELECT * FROM commands", sql_connection);
            data_adapter.Fill(data_set, "commands");

            #region Menu init

            Console.Write("Init MenutItems: ");

            foreach (DataRow element in data_set.Tables["commands"].Rows)
            {
                menuItems.Add(new MenuItems(element.ItemArray[1].ToString(), element.ItemArray[2].ToString(),
                    element.ItemArray[3].ToString(), element.ItemArray[4].ToString()));
            }
            menuItems.Add(new MenuItems("/button", "Test comand", "Test inline buttons", InlineKeyboardButton.WithCallbackData("Testo"), "Testo"));
            menuItems.Add(new MenuItems("/help", "Help", "Show help page", MenuItems.CunstructHelpPage(menuItems)));

            ConsoleOutputs.Output(ConsoleOutputs.OutputType.Completed);

            #endregion

            Console.WriteLine("Bot init finshed");
            Console.WriteLine("-----------------------------------------------------\n" +
                              "!!!!!AT ANY TIME, TYPE ENTER KEY TO STOP THE BOT!!!!!\n" +
                              "-----------------------------------------------------\n");



            //Da utilizzare qualcosa di meglio come una command line
            Console.ReadLine();

            ClientBot.StopReceiving();
        }

        #region Events

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
        private static void ClientBot_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            Console.WriteLine("[" + DateTime.Now + "]" +
                " Chat message recived: " + e.CallbackQuery.Data +
                "\t| From: " + e.CallbackQuery.From.Username +
                " Id: " + e.CallbackQuery.Id);

           InlineKeyboardMarkup Domanda = new InlineKeyboardMarkup(
           new InlineKeyboardButton[][]
           {
                    new InlineKeyboardButton[]
                        {
                            new InlineKeyboardCallbackButton("✅","yes"),
                            new InlineKeyboardCallbackButton("❎","not"),
                        },
                   });


            InlineKeyboardMarkup Indietro = new InlineKeyboardMarkup(
           new InlineKeyboardButton[][]
           {
                    new InlineKeyboardButton[]
                        {
                            new InlineKeyboardCallbackButton("Torna indietro 🔙","back"),
                        },
                   });

            InlineKeyboardMarkup inizio = new InlineKeyboardMarkup(  //Clone di Menuinizio, serve per callbackquery di tastiera inline
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

            switch (e.CallbackQuery.Data)
            {
                case "rand":
                    ClientBot.EditMessageTextAsync(e.CallbackQuery.From.Id, e.CallbackQuery.Message.MessageId, "Saltor il folle, 🃏 " +
                    "So per certo che il generale Marcus sta organizzando una rivolta per spodestrarla e prendere il suo posto.");
                    ClientBot.EditMessageReplyMarkupAsync(e.CallbackQuery.From.Id, e.CallbackQuery.Message.MessageId, replyMarkup: Domanda);
                    break;

                case "story":
                    ClientBot.EditMessageTextAsync(e.CallbackQuery.From.Id, e.CallbackQuery.Message.MessageId, "Questa modalità è ancora in fase di sviluppo. Stay tuned");
                    ClientBot.EditMessageReplyMarkupAsync(e.CallbackQuery.From.Id, e.CallbackQuery.Message.MessageId, replyMarkup: Indietro);
                    break;

                case "rules":
                    ClientBot.EditMessageTextAsync(e.CallbackQuery.From.Id, e.CallbackQuery.Message.MessageId, "Ogni anno arriva una nuova \n" +
                        "importante e apparentemente casuale richiesta dal tuo imprevedibile regno, mentre dovrai fare tutto il possibile per " +
                        "mantenere l'equilibrio tra le seguenti classi:\n• Chiesa ⛪️ \n• Popolo 👨‍👩‍👧‍👦\n• Esercito ⚔️\n• Denaro 💰\nDecisioni " +
                        "attente e pianificazione ti porteranno a regnare a lungo, ma cause impreviste, eventi a sorpresa e scarsa fortuna " +
                        "possono far cadere anche il più radicato dei monarchi.Rimani a capo del tuo regno per più tempo possibile, crea alleanze, " +
                        "fatti dei nemici e trova nuovi modi per morire mentre la tua dinastia si fa strada negli anni.");
                    ClientBot.EditMessageReplyMarkupAsync(e.CallbackQuery.From.Id, e.CallbackQuery.Message.MessageId, replyMarkup: Indietro);
                    break;

                case "back":
                    ClientBot.EditMessageTextAsync(e.CallbackQuery.From.Id, e.CallbackQuery.Message.MessageId, Str);
                    ClientBot.EditMessageReplyMarkupAsync(e.CallbackQuery.From.Id, e.CallbackQuery.Message.MessageId, replyMarkup: inizio);
                    break;

                default:
                    ClientBot.SendTextMessageAsync(e.CallbackQuery.From.Id, "Qualcosa è andato storto, riprova", replyMarkup: Indietro);
                    break;


            }
            
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
            ClientBot.SendTextMessageAsync(e.Message.Chat.Id, Str, replyMarkup: menuinizio);


        }

        #endregion

        #region Methods
        private static MySqlConnection ConnectToDb(string database = "",string userID = "" , string server = "", string password = "", MySqlSslMode mode = MySqlSslMode.None)
        { 
            MySqlConnectionStringBuilder connectionString;
            MySqlConnection connection = new MySqlConnection();

            bool success = false;
            do
            {
                try
                {
                    if (userID == "")
                    {
                        Console.Write("UserID (i.e. root, jhon, etc): "); Console.ForegroundColor = ConsoleColor.DarkYellow; userID = Console.ReadLine();
                        Console.ForegroundColor = console_default_color;
                    }
                    if (database == "")
                    {
                        Console.Write("Database (i.e. db_reigns, etc): "); Console.ForegroundColor = ConsoleColor.DarkYellow; database = Console.ReadLine();
                        Console.ForegroundColor = console_default_color;
                    }
                    if (server == "")
                    {
                        Console.Write("Server (ip where db server is located): "); Console.ForegroundColor = ConsoleColor.DarkYellow; server = Console.ReadLine();
                        Console.ForegroundColor = console_default_color;
                    }
                    if (password == "")
                    {
                        Console.Write("Password: "); //password = Console.ReadLine();
                        InputPassword(password);
                        Console.WriteLine();
                    }

                    connectionString = new MySqlConnectionStringBuilder
                    {
                        Server = server,
                        UserID = userID,
                        Password = password,
                        Database = database,
                        SslMode = mode
                    };

                    connection = new MySqlConnection(connectionString.ConnectionString);
                    Console.WriteLine("Connecting to db...");
                    connection.Open();
                    success = true;
                }
                catch (Exception ex)
                {

                    Console.Write("Errore durante la connessione al server: " + ex.Message +
                                  "\n Riprovare la connessione? (y/P(solo password)/n): ");
                    ConsoleKeyInfo key =  Console.ReadKey();
                    Console.WriteLine();
                    if (key.KeyChar == 'n') break;
                    if (key.KeyChar == 'y')
                    {
                        userID = "";
                        database = "";
                        server = "";
                        password = "";
                    }
                }

            } while (!success);
            Console.Write("Connection to DB: ");
            if (success) ConsoleOutputs.Output(ConsoleOutputs.OutputType.Completed);
            else ConsoleOutputs.Output(ConsoleOutputs.OutputType.Interrupted);

            return connection;
        }

        public static void InputPassword(string password)
        {

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }

            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
        }
        #endregion
    }
}