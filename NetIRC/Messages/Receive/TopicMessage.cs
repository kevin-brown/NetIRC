using System;
using System.Linq;

namespace NetIRC.Messages.Receive
{
    class TopicMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "TOPIC";
        }

        public override void ProcessMessage(ParsedMessage message, Client client)
        {
            User user = message.GetUser();
            Channel channel = message.GetChannel();
            string topic = message.Parameters[1];

            channel.Topic.Message = topic;
            channel.Topic.Author = user;
            channel.Topic.LastUpdated = DateTime.Now;

            channel.TriggerOnTopicChange(channel.Topic);
        }
    }
}
