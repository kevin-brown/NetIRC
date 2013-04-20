using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class PingMessage : ReceiveMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            if (message.StartsWith("PING"))
            {
                return true;
            }

            return false;
        }

        public void ProcessMessage(string message, Client client)
        {
            string[] args = message.Split(' ');

            client.Send(new Messages.Send.PongMessage(args[1]));
        }
    }
}
