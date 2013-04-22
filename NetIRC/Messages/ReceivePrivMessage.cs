using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages
{
    abstract class ReceivePrivMessage : ReceiveUserMessage
    {
        public static Channel GetChannel(string message)
        {
            string[] parts = message.Split(' ');

            if (!parts[2].StartsWith("#"))
            {
                return null;
            }

            return ChannelFactory.FromName(parts[2].Substring(1));
        }

        public static bool IsCTCP(string message)
        {
            string[] parts = message.Split(' ');

            return parts[3].StartsWith(":\x001");
        }
    }
}
