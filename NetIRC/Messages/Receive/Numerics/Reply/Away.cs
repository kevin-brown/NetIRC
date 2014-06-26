using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetIRC.Messages.Receive.Numerics.Reply
{
    public class Away : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "301";
        }

        #region IReceiveMessage Members

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User target = message.GetUserFromNick(message.Parameters[1]);

            target.AwayMessage = message.Parameters[2];
        }

        #endregion
    }
}
