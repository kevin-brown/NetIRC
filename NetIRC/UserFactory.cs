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
            this._client = client;
        }

        internal void RemoveNick(string nick)
        {
            this._store.Remove(nick);
        }

        internal User ChangeNick(string original, string future)
        {
            User user = this.FromNick(original);

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
            Match matches = Regex.Match(userMask, @"^(?<nickname>[a-z0-9_\-\[\]\\^{}|`]+)!(?<username>[a-z0-9_\-\~]+)\@(?<hostname>[a-z0-9\.\-]+)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            if (!matches.Success)
            {
                return null;
            }

            string nickname = matches.Groups["nickname"].Value;
            string username = matches.Groups["username"].Value;
            string hostname = matches.Groups["hostname"].Value;

            User user = this.FromNick(nickname);
            user.UserName = username;
            user.HostName = hostname;

            return user;
        }

        public Dictionary<string, User> InChannel(string channel)
        {
            return this._store.Where(
                u => u.Value._channels.ContainsKey(channel))
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
