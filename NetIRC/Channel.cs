using System;
using System.Collections.Generic;

namespace NetIRC
{
    class Channel
    {
        public string Topic
        {
            get;
            private set;
        }

        public List<User> Users
        {
            get;
            private set;
        }
    }
}
