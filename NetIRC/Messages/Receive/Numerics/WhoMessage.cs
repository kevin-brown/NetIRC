﻿using System;
using System.Linq;

namespace NetIRC.Messages.Receive.Numerics
{
    class WhoMessage : ReceiveNumericMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            return ReceiveNumericMessage.CheckNumeric(message, client, 352);
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            Channel channel = ChannelFactory.FromName(parts[3].ToLower().Substring(1));

            User oldUser = UserFactory.FromNick(parts[7]);

            oldUser.HostName = parts[5];
            oldUser.UserName = parts[4];

            if (parts[8].Length > 1)
            {
                char rankChar = parts[8][1];

                if (rankChar == '*')
                {
                    oldUser.IsOperator = true;

                    if (parts[8].Length > 2)
                    {
                        rankChar = parts[8][2];
                    }
                }
                else
                {
                    oldUser.IsOperator = false;
                }

                if (rankChar != '*')
                {
                    UserRank rank = User.RankChars[rankChar];

                    oldUser.Rank[channel.Name] = rank;
                }
            }

            string realName = "";

            for (int i = 10; i < parts.Length; i++)
            {
                realName += parts[i] + " ";
            }

            realName = realName.Trim();
            oldUser.RealName = realName;

            client.TriggerOnWho(string.Join(" ", parts.Skip(3)));
            channel.TriggerOnWho(string.Join(" ", parts.Skip(3)));
        }
    }
}
