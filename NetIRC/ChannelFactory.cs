using System;
using System.Collections.Generic;
using System.Linq;

namespace NetIRC
{
    internal class ChannelFactory
    {
        private readonly Client _client;

        private readonly Dictionary<string, Channel> _store = new Dictionary<string, Channel>(StringComparer.InvariantCultureIgnoreCase);

        public ChannelFactory(Client client)
        {
            this._client = client;
        }

        public void RemoveName(string name)
        {
            this._store.Remove(name);
        }

        public Channel FromName(string name)
        {
            if (this._store.ContainsKey(name))
            {
                return this._store[name];
            }

            Channel channel = new Channel(name) {Client = this._client};
            this._store[name] = channel;

            return channel;
        }

        public Channel FromFullName(string name)
        {
            if (this._store.ContainsKey(name))
            {
                return this._store[name];
            }

            if (!Channel.TypeChars.Values.Contains(name[0]))
                throw new ArgumentException("name");

            Channel channel = new Channel(name) { Client = this._client };
            this._store[name.Substring(1)] = channel;

            return channel;
        }


        public Dictionary<string, Channel> HasUser(User user)
        {
            return this._store.Where(c => c.Value.Users.ContainsKey(user.NickName)).ToDictionary(c => c.Key, c => c.Value, StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
