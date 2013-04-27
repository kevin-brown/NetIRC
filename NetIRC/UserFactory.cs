using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetIRC
{
    internal class UserFactory
    {
        private static Dictionary<string, User> Store = new Dictionary<string, User>(StringComparer.InvariantCultureIgnoreCase);

        internal static User ChangeNick(string original, string future)
        {
            User user = FromNick(original);

            Store[future] = user;
            Store.Remove(user.NickName);

            user.NickName = future;

            return user;
        }

        public static User FromNick(string nick)
        {
            if (Store.ContainsKey(nick))
            {
                return Store[nick];
            }

            User user = new User(nick);
            Store[nick] = user;

            user.Rank = UserRank.None;

            return user;
        }

        public static User FromUserMask(string userMask)
        {
            Match matches = Regex.Match(userMask, @"^([A-Za-z0-9\-]+)!([A-Za-z0-9\-\~]+)\@([A-Za-z0-9\.\-]+)", RegexOptions.IgnoreCase);

            if (!matches.Success)
            {
                return null;
            }

            User user = FromNick(matches.Groups[1].Value);

            if (user == null)
            {
                string nick = matches.Groups[1].Value;

                user = new User(nick, matches.Groups[3].Value, matches.Groups[2].Value);
                Store[nick] = user;
            }

            return user;
        }

        public static Dictionary<string, User> InChannel(string channel)
        {
            return Store.Where(u => u.Value.Channels.Contains(ChannelFactory.FromName(channel))).ToDictionary(u => u.Key, u => u.Value, StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
