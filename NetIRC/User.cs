using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        public static User FromUserMask(string userMask)
        {
            Match matches = Regex.Match(userMask, @"^([A-Za-z0-9\-]+)!([A-Za-z0-9\-\~]+)\@([A-Za-z0-9\.\-]+)", RegexOptions.IgnoreCase);

            if (!matches.Success)
            {
                return null;
            }

            return new User(matches.Groups[1].Value, matches.Groups[3].Value, matches.Groups[2].Value);
        }
    }
}
