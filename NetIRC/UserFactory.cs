using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetIRC
{
    internal class UserFactory
    {
        private readonly Client _client;

        private readonly Dictionary<string, User> _store = new Dictionary<string, User>(StringComparer.InvariantCultureIgnoreCase);

        public UserFactory(Client client)
        {
            _client = client;
        }

        internal User RemoveNick(string nick)
        {
            User user = FromNick(nick);

            this._store.Remove(nick);

            return user;
        }

        internal User ChangeNick(string original, string future)
        {
            User user = FromNick(original);

            this._store[future] = user;
            this._store.Remove(user.NickName);

            user.NickName = future;

            return user;
        }

        public User FromNick(string nick)
        {
            if (this._store.ContainsKey(nick))
            {
                return this._store[nick];
            }

            User user = new User(nick);
            this._store[nick] = user;

            return user;
        }

        public User FromUserMask(string userMask)
        {
            Match matches = Regex.Match(userMask, @"^([A-Za-z0-9_\-\[\]\\^{}|`]+)!([A-Za-z0-9_\-\~]+)\@([A-Za-z0-9\.\-]+)", RegexOptions.IgnoreCase);

            if (!matches.Success)
            {
                return null;
            }

            User user = FromNick(matches.Groups[1].Value);

            if (user == null)
            {
                string nick = matches.Groups[1].Value;

                user = new User(nick, matches.Groups[3].Value, matches.Groups[2].Value);
                this._store[nick] = user;
            }

            return user;
        }

        public Dictionary<string, User> InChannel(string channel)
        {
            return this._store.Where(
                u => u.Value.Channels.Contains(
                    _client.ChannelFactory.FromName(channel)))
                        .ToDictionary(
                            u => u.Key,
                            u => u.Value,
                            StringComparer.InvariantCultureIgnoreCase);
        }

        internal void SetUser(string nick, User user)
        {
            this._store[nick] = user;
        }
    }
}
