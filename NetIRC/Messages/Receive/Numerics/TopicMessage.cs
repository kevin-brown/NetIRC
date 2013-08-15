using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive.Numerics
{
    class TopicMessage : ReceiveNumericMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            return ReceiveNumericMessage.CheckNumeric(message, client, 332);
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            Channel channel = ChannelFactory.FromName(parts[3].Substring(1));
            string topic = String.Join(" ", parts.Skip(4)).Substring(1);

            channel.Topic.Message = topic;
        }
    }
}
