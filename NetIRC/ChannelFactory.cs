using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC
{
    class ChannelFactory
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
    }
}
