using System;

namespace NetIRC.Messages.Receive.Numerics
{
    public class TopicInfo : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "333";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User target = message.GetUserFromNick(message.Parameters[0]);

            if (target == client.User)
            {
                Channel channel = message.GetChannel(message.Parameters[1]);
                User user = message.GetUser(message.Parameters[2]);
                int timestamp = int.Parse(message.Parameters[3]);

                DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                time = time.AddSeconds(timestamp);

                channel.Topic.Author = user;
                channel.Topic.LastUpdated = time.ToLocalTime();
                channel.TriggerOnTopic(channel.Topic);
            }

        }
    }
}
