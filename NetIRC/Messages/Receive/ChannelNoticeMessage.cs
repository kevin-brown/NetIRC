using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class ChannelNoticeMessage : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "NOTICE" &&
                   message.IsChannel() &&
                   !message.IsCTCP();
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User user = message.GetUser();
            Channel channel = message.GetChannel();
            string notice = message.Parameters[1];

            channel.TriggerOnNotice(user, notice);
        }
    }
}
