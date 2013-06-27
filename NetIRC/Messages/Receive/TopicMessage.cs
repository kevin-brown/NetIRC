using System;
using System.Linq;

namespace NetIRC.Messages.Receive
{
    class TopicMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            return ReceiveUserMessage.CheckCommand(message, "TOPIC");
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            Channel channel = ChannelFactory.FromName(parts[2].Substring(1));
            User user = ReceiveUserMessage.GetUser(message);

            string topic = String.Join(" ", parts.Skip(3).ToArray()).Substring(1);

            channel.Topic.Message = topic;
            channel.Topic.Author = user;
            channel.Topic.LastUpdated = DateTime.Now;

            channel.TriggerOnTopicChange(channel.Topic);
        }
    }
}
