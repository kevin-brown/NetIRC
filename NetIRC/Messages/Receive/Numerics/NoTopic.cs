using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive.Numerics
{
    class NoTopic : ReceiveNumericMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            return ReceiveNumericMessage.CheckNumeric(message, client, 331);
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            Channel channel = ChannelFactory.FromName(parts[3].Substring(1));

            channel.Topic.ClearTopic();
        }
    }
}
