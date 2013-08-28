using System;
using System.Linq;

namespace NetIRC.Messages.Receive.Numerics
{
    class NamesMessage : ReceiveNumericMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            return ReceiveNumericMessage.CheckNumeric(message, client, 353);
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            Channel channel = client.ChannelFactory.FromName(parts[4].ToLower().Substring(1));
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
                    rank = User.RankChars[firstChar];

                    parts[i] = parts[i].Substring(1);
                }

                User user = client.UserFactory.FromNick(parts[i]);
                user._ranks[channel.Name] = rank;

                channel.AddUser(user);
            }
        }
    }
}
