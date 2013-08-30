using System;

namespace NetIRC.Messages.Receive
{
    class Topic : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "TOPIC";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
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
