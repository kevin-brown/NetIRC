using System;
using System.Collections.Generic;

namespace NetIRC
{
    public class User
    {
        public string NickName
        {
            get;
            internal set;
        }

        public string UserName
        {
            get;
            internal set;
        }

        public string HostName
        {
            get;
            internal set;
        }

        public string RealName
        {
            get;
            internal set;
        }

        public readonly List<Channel> Channels = new List<Channel>();

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

        public delegate void OnNickChangeHandler(User user, string original);
        public event OnNickChangeHandler OnNickChange;

        public void TriggerOnNickChange(string original)
        {
            if (OnNickChange != null)
            {
                OnNickChange(this, original);
            }
        }
    }
}
