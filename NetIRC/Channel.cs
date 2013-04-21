using System;
using System.Collections.Generic;

namespace NetIRC
{
    public class Channel
    {
        public string Name
        {
            get;
            private set;
        }

        public string Topic
        {
            get;
            private set;
        }

        public Dictionary<string, User> Users
        {
            get;
            private set;
        }

        public Channel(string name)
        {
            this.Name = name;

            this.Users = new Dictionary<string, User>();
        }

        internal void AddUser(User user)
        {
            if (this.Users.ContainsKey(user.NickName))
            {
                return;
            }

            this.Users.Add(user.NickName, user);
        }
    }
}
