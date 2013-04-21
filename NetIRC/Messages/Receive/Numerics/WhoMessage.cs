using System;

namespace NetIRC.Messages.Receive.Numerics
{
    class WhoMessage : ReceiveNumericMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            return ReceiveNumericMessage.CheckNumeric(message, server, 352);
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            Channel channel = client.Channels[parts[3].ToLower().Substring(1)];

            User oldUser = channel.Users[parts[7]];

            oldUser.HostName = parts[5];
            oldUser.UserName = parts[4];
        }
    }
}
