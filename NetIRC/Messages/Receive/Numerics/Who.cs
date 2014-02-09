using System.Linq;

namespace NetIRC.Messages.Receive.Numerics
{
    public class Who : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "352";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            Channel channel = message.GetChannel(message.Parameters[1]);
            User user = message.GetUserFromNick(message.Parameters[5]);

            user.HostName = message.Parameters[3];
            user.UserName = message.Parameters[2];
            user.RealName = string.Join(" ", message.Parameters.Skip(8)).Trim();
            user.IsOperator = message.Parameters[6].Contains('*');

            foreach (char c in message.Parameters[6])
            {
                switch (c)
                {
                    case 'G':
                        user.IsAway = true;
                        break;
                    case 'H':
                        user.IsAway = false;
                        break;
                    default:
                        if (User.RankChars.ContainsKey(c))
                        {
                            UserRank rank = User.RankChars[c];

                            channel.SetRank(user, user.Ranks[channel.Name] | User.RankChars[c]);
                        }
                        break;
                }
            }

            client.TriggerOnWho(user, string.Join(" ", message.Parameters.Skip(3)));
            channel.TriggerOnWho(user, string.Join(" ", message.Parameters.Skip(3)));
        }
    }
}
