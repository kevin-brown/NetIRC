using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class ChatMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            if (!ReceiveUserMessage.CheckCommand(message, "PRIVMSG"))
            {
                return false;
            }

            string[] parts = message.Split(' ');

            if (!parts[2].Contains("#"))
            {
                return false;
            }

            return true;
        }

        public override void ProcessMessage(string message, Client client)
        {
            User user = this.GetUser(message);

            string[] parts = message.Split(' ');

            string line = String.Join(" ", parts.Skip(3).ToArray()).Substring(1);

            Console.WriteLine("{0} said {1}", user.NickName, line);
        }
    }
}
