using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetIRC.Messages.Receive.IRCv3
{
    public class Away : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "AWAY";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User user = message.GetUser();

            if (message.Parameters.Length == 0)
            {
                user.IsAway = false;
                user.AwayMessage = null;
            }
            else
            {
                user.IsAway = true;
                user.AwayMessage = message.Parameters[0];
            }
        }
    }
}
