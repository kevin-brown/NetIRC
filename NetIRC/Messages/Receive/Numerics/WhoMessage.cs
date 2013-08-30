using System;
using System.Linq;

namespace NetIRC.Messages.Receive.Numerics
{
    class WhoMessage : ReceiveNumericMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "352";
        }

        //TODO: Do some code cleanup
        public override void ProcessMessage(ParsedMessage message, Client client)
        {
            Channel channel = message.GetChannel(message.Parameters[1]);
            User oldUser = message.GetUserFromNick(message.Parameters[5]);

            oldUser.HostName = message.Parameters[3];
            oldUser.UserName = message.Parameters[2];

            if (message.Parameters[6].Length > 1)
            {
                char rankChar = message.Parameters[6][1];

                if (rankChar == '*')
                {
                    oldUser.IsOperator = true;

                    if (message.Parameters[6].Length > 2)
                    {
                        rankChar = message.Parameters[6][2];
                    }
                }
                else
                {
                    oldUser.IsOperator = false;
                }

                if (rankChar != '*')
                {
                    UserRank rank = User.RankChars[rankChar];

                    oldUser._ranks[channel.Name] = rank;
                }
            }
            oldUser.RealName = string.Join(" ", message.Parameters.Skip(8)).Trim();

            client.TriggerOnWho(string.Join(" ", message.Parameters.Skip(3)));
            channel.TriggerOnWho(string.Join(" ", message.Parameters.Skip(3)));
        }
    }
}
