using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive.Numerics
{
    class NoTopic : ReceiveNumericMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "331";
        }

        public override void ProcessMessage(ParsedMessage message, Client client)
        {
            User target = message.GetUserFromNick(message.Parameters[0]);

            if (target == client.User)
            {
                Channel channel = message.GetChannel(message.Parameters[1]);

                channel.Topic.ClearTopic();
            }
        }
    }
}
