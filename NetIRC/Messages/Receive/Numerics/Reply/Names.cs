using System;
using System.Linq;

namespace NetIRC.Messages.Receive.Numerics.Reply
{
    public class Names : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "353";
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User target = message.GetUserFromNick(message.Parameters[0]);

            if (target == client.User)
            {
                char chanType = Convert.ToChar(message.Parameters[1]);
                Channel channel = message.GetChannel(message.Parameters[2]);

                switch (chanType)
                {
                    case '=':
                        channel.IsPrivate = false;
                        channel.IsSecret = false;
                        break;
                    case '*':
                        channel.IsPrivate = true;
                        channel.IsSecret = false;
                        break;
                    case '@':
                        channel.IsSecret = true;
                        break;
                }

                
                string[] users = message.Parameters[3].Split(' ');

                foreach (string userStr in users)
                {
                    if (string.IsNullOrEmpty(userStr))
                    {
                        break;
                    }

                    UserRank rank = UserRank.None;
                    string nick = userStr;

                    char[] ranks = userStr.TakeWhile(c => User.RankChars.ContainsKey(c)).ToArray(); // Take all the user statuses
                    nick = new string(userStr.SkipWhile(c => User.RankChars.ContainsKey(c)).ToArray()); // Take the remaining nick

                    User user = client.UserFactory.FromNick(nick);

                    foreach (char rankChar in ranks)
                    {
                        rank = rank | User.RankChars[rankChar];
                    }

                    channel.SetRank(user, rank);
                    channel.AddUser(user);
                }

                channel.TriggerOnNames(users);
            }
        }
    }
}
