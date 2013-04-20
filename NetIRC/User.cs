using System;
using System.Collections.Generic;

namespace NetIRC
{
    public class User
    {
        public string NickName
        {
            get;
            private set;
        }

        public string UserName
        {
            get;
            private set;
        }

        public string HostName
        {
            get;
            private set;
        }

        public string RealName
        {
            get;
            private set;
        }

        public List<Channel> Channels
        {
            get;
            private set;
        }

        public User()
        {
            
        }

        public User(string nick)
        {
            this.NickName = nick;
            this.RealName = nick;
            this.UserName = nick;
        }

        public User(string nick, string user)
        {
            this.NickName = nick;
            this.RealName = nick;
            this.UserName = user;
        }

        public User(string nick, string user, string real)
        {
            this.NickName = nick;
            this.RealName = real;
            this.UserName = user;
        }
    }
}
