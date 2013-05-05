using System;
using System.Collections.Generic;

namespace NetIRC
{
    public class User
    {
        /// <summary>
        /// The nickname that the user will be associated with.
        /// </summary>
        public string NickName
        {
            get;
            internal set;
        }

        /// <summary>
        /// The username (sometimes called ident) that is associated with the user.
        /// </summary>
        public string UserName
        {
            get;
            internal set;
        }

        /// <summary>
        /// The hostname that is attached to the user.
        /// </summary>
        public string HostName
        {
            get;
            internal set;
        }

        /// <summary>
        /// The real name that the user specified when connecting.
        /// </summary>
        public string RealName
        {
            get;
            internal set;
        }

        /// <summary>
        /// The list of channels that the user is in.
        /// </summary>
        public readonly List<Channel> Channels = new List<Channel>();

        /// <summary>
        /// The rank that is attached to this user.
        /// </summary>
        public UserRank Rank
        {
            get;
            internal set;
        }

        /// <summary>
        /// A dictionary that represents the characters associated with specific ranks in WHO and NAMES messages.
        /// </summary>
        internal static Dictionary<char, UserRank> RankChars = new Dictionary<char, UserRank>()
            {
                {'+', UserRank.Voice},
                {'%', UserRank.HalfOp},
                {'@', UserRank.Op},
                {'&', UserRank.Admin},
                {'~', UserRank.Owner},
            };

        /// <summary>
        /// A dictionary representing the characters that are used to change a user's rank through channel modes.
        /// </summary>
        internal static Dictionary<char, UserRank> RankModes = new Dictionary<char, UserRank>()
            {
                {'v', UserRank.Voice},
                {'h', UserRank.HalfOp},
                {'o', UserRank.Op},
                {'a', UserRank.Admin},
                {'q', UserRank.Owner},
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

        internal void TriggerOnNickChange(string original)
        {
            if (OnNickChange != null)
            {
                OnNickChange(this, original);
            }
        }

        public delegate void OnQuitHandler(User user, string reason);
        public event OnQuitHandler OnQuit;

        internal void TriggerOnQuit(string reason)
        {
            if (OnQuit != null)
            {
                OnQuit(this, reason);
            }
        }

        public delegate void OnVersionHandler(User target, User source);
        public event OnVersionHandler OnVersion;

        internal void TriggerOnVersion(User source)
        {
            if (OnVersion != null)
            {
                OnVersion(this, source);
            }
        }

        public delegate void OnVersionReplyHandler(User target, User source, string version);
        public event OnVersionReplyHandler OnVersionReply;

        internal void TriggerOnVersionReply(User source, string version)
        {
            if (OnVersionReply != null)
            {
                OnVersionReply(this, source, version);
            }
        }
    }
}
