using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC
{
    internal class ChannelFactory
    {
        private static Dictionary<string, Channel> Store = new Dictionary<string, Channel>();

        public static Channel FromName(string name)
        {
            if (Store.ContainsKey(name))
            {
                return Store[name];
            }

            Channel channel = new Channel(name);
            Store[name] = channel;

            return channel;
        }

        public static Dictionary<string, Channel> HasUser(User user)
        {
            return Store.Where(c => c.Value.Users.ContainsKey(user.NickName)).ToDictionary(c => c.Key, c => c.Value);
        }
    }
}
