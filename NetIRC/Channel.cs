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
            get
            {
                return UserFactory.InChannel(this.Name);
            }
        }

        public Channel(string name)
        {
            this.Name = name;
        }

        internal void AddUser(User user)
        {
            if (this.Users.ContainsKey(user.NickName))
            {
                return;
            }

            user.Channels.Add(this);
        }

        internal void RemoveUser(User user)
        {
            if (this.Users.ContainsKey(user.NickName))
            {
                user.Channels.Remove(this);
            }
        }

        internal Messages.SendMessage SendWho()
        {
            return new Messages.Send.WhoMessage("#" + this.Name);
        }
    }
}
