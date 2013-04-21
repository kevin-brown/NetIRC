using System;

namespace NetIRC.Messages.Receive.Numerics
{
    class NamesMessage : ReceiveNumericMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            return ReceiveNumericMessage.CheckNumeric(message, server, 353);
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            Channel channel = client.Channels[parts[4].ToLower().Substring(1)];
            parts[5] = parts[5].Substring(1);

            for (int i = 5; i < parts.Length; i++)
            {
                if (string.IsNullOrEmpty(parts[i]))
                {
                    break;
                }

                if (!Char.IsLetter(parts[i].ToCharArray()[0]))
                {
                    parts[i] = parts[i].Substring(1);
                }

                User user = UserFactory.FromNick(parts[i]);

                channel.AddUser(user);
            }

            client.Send(channel.SendWho());
        }
    }
}
