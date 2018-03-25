using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.InlineKeyboardButtons;

namespace ReignsBot.classes
{
    class MenuItems
    {
        string _trigger;        //text wich will trigger this item  (es. "/start")
        string _commandName;    //command name  (es. Start)
        string _commandDesc;    //command description (es. This command will show Starting information)
        Output _output;            //command output when triggered (es. "Hello, this is the start command")

        /// <summary>
        /// Init of a new Menu Item
        /// </summary>
        /// <param name="newTrigger">text wich will trigger this item  (es. "/start")</param>
        /// <param name="newCommandName">command name  (es. Start)</param>
        /// <param name="newCommandDesc">command description (es. This command will show Starting information)</param>
        /// <param name="newOutput">command output when triggered (es. "Hello, this is the start command")</param>
        public MenuItems(string newTrigger, string newCommandName, string newCommandDesc, string newOutput)
        {
            _trigger = newTrigger;
            _commandName = newCommandName;
            _commandDesc = newCommandDesc;
            _output = new Output(newOutput);
        }

        public MenuItems(string newTrigger, string newCommandName, string newCommandDesc,InlineKeyboardButton newOutput)
        {
            _trigger = newTrigger;
            _commandName = newCommandName;
            _commandDesc = newCommandDesc;
            _output = new  Output(newOutput);
        }

        public static string CunstructHelpPage(List<MenuItems> CommandList)
        {
            string HelpPage = "";

            foreach(MenuItems command in CommandList)
            {
                HelpPage += "\t\tCOMMAND LIST\t\t\n" +
                "\t" + command.CommandName + "\n" +
                "\t\t\t\tDesc:" + command.CommandDesc + "\n" +
                "\t\t\t\tUsage:" + command.Trigger + "\n";
            }
            return HelpPage;
        }

        public string Trigger { get => _trigger; }
        public string CommandName { get => _commandName; }
        public string CommandDesc { get => _commandDesc; }
        public Output Output { get => _output; }
    }

    class Output
    {
        public enum OutType
        {
            IsString,
            IsInlineKeyboardButton
        }

        OutType _type;
        InlineKeyboardButton tInlineKeyboardButton;
        string tString;


        public Output(string newTOut)
        {
            _type = OutType.IsString;
            tString = newTOut;
        }

        public Output(InlineKeyboardButton newTOut)
        {
            _type = OutType.IsInlineKeyboardButton;
            tInlineKeyboardButton = newTOut;
        }

        public OutType Type { get => _type; }
        public string OutString { get => tString; }
        public InlineKeyboardButton OutInlineKeyboardButton { get => tInlineKeyboardButton; }

    }       
}
