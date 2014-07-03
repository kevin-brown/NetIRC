using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetIRC.Messages.Receive.Numerics.Reply
{
    public class Banlist : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "367";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            Channel target = message.GetChannel(message.Parameters[0]);

            target.BanList.Add(message.Parameters[1]);
        }
    }
}
