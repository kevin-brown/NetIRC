using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive.Numerics
{
    class TopicInfo : ReceiveNumericMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            return ReceiveNumericMessage.CheckNumeric(message, client, 333);
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            Channel channel = client.ChannelFactory.FromName(parts[3].Substring(1));

            User user = client.UserFactory.FromUserMask(parts[4]);

            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            time = time.AddSeconds(int.Parse(parts[5]));

            channel.Topic.Author = user;
            channel.Topic.LastUpdated = time.ToLocalTime();

            channel.TriggerOnTopicChange(channel.Topic);
        }
    }
}
