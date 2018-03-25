using System;
using System.Collections.Generic;
using System.Text;

namespace ReignsBot.classes
{
    class MenuItems
    {
        string _trigger;        //text wich will trigger this item  (es. "/start")
        string _commandName;    //command name  (es. Start)
        string _commandDesc;    //command description (es. This command will show Starting information)
        string _output;         //command output when triggered (es. "Hello, this is the start command")

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
            _output = newOutput;
        }

        public string Trigger { get => _trigger; }
        public string CommandName { get => _commandName; }
        public string CommandDesc { get => _commandDesc; }
        public string Output { get => _output; }
    }
}
