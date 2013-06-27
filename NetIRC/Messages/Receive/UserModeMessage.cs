using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class UserModeMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            string[] parts = message.Split(' ');

            return ReceiveUserMessage.CheckCommand(message, "MODE") &&
                parts[2].StartsWith("#") == false; //it's not a channel
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            string modes = parts[3];

            client.TriggerOnUserMode(modes);
        }
    }
}
