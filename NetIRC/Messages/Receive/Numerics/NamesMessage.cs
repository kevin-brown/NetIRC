using System;
using System.Linq;

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

            Channel channel = ChannelFactory.FromName(parts[4].ToLower().Substring(1));
            parts[5] = parts[5].Substring(1);

            for (int i = 5; i < parts.Length; i++)
            {
                if (string.IsNullOrEmpty(parts[i]))
                {
                    break;
                }

                UserRank rank = UserRank.None;

                if (!Char.IsLetter(parts[i].ToCharArray()[0]))
                {
                    char firstChar = parts[i][0];
                    rank = User.Ranks.FirstOrDefault(r => r.Value == firstChar).Key;

                    parts[i] = parts[i].Substring(1);
                }

                User user = UserFactory.FromNick(parts[i]);
                user.Rank = rank;

                channel.AddUser(user);
            }

            client.Send(channel.SendWho());
        }
    }
}
