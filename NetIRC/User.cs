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

        public UserRank Rank
        {
            get;
            internal set;
        }

        internal static Dictionary<UserRank, string> Ranks = new Dictionary<UserRank, string>()
            {
                {UserRank.None, ""},
                {UserRank.Voice, "v"},
                {UserRank.HalfOp, "h"},
                {UserRank.Op, "o"},
                {UserRank.Admin, "a"},
                {UserRank.Owner, "q"},
            };

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

        public delegate void OnQuitHandler(User user, string reason);
        public event OnQuitHandler OnQuit;

        public void TriggerOnQuit(string reason)
        {
            if (OnQuit != null)
            {
                OnQuit(this, reason);
            }
        }

        public delegate void OnVersionHandler(User target, User source);
        public event OnVersionHandler OnVersion;

        public void TriggerOnVersion(User source)
        {
            if (OnVersion != null)
            {
                OnVersion(this, source);
            }
        }

        public delegate void OnVersionReplyHandler(User target, User source, string version);
        public event OnVersionReplyHandler OnVersionReply;

        public void TriggerOnVersionReply(User source, string version)
        {
            if (OnVersionReply != null)
            {
                OnVersionReply(this, source, version);
            }
        }
    }
}
