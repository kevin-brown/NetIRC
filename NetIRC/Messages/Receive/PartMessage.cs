using System;
using System.Linq;

namespace NetIRC.Messages.Receive
{
    class PartMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            return ReceiveUserMessage.CheckCommand(message, "PART");
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');
            Channel channel = client.Channels[parts[2].ToLower().Substring(1)];

            User user = this.GetUser(message);

            channel.RemoveUser(user);
        }
    }
}
