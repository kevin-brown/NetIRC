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

        private string _userName = string.Empty;

        /// <summary>
        /// The username (sometimes called ident) that is associated with the user.
        /// </summary>
        public string UserName
        {
            get
            {
                return this._userName;
            }

            internal set
            {
                if (this._userName == value)
                {
                    return;
                }

                string original = this._userName;

                this._userName = value;

                this.TriggerOnUserNameChange(original);
            }
        }

        private string _hostName = string.Empty;

        /// <summary>
        /// The hostname that is attached to the user.
        /// </summary>
        public string HostName
        {
            get
            {
                return this._hostName;
            }

            internal set
            {
                if (this._hostName == value)
                {
                    return;
                }

                string original = this._hostName;

                this._hostName = value;

                this.TriggerOnHostNameChange(original);
            }
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
        /// The user is away.
        /// </summary>
        public bool IsAway
        {
            get;
            internal set;
        }

        /// <summary>
        /// The user is marked as invisible.
        /// </summary>
        public bool IsInvisible
        {
            get;
            internal set;
        }

        /// <summary>
        /// The user is receiving WallOps.
        /// </summary>
        public bool IsReceivingWallOps
        {
            get;
            internal set;
        }

        /// <summary>
        /// The user is restricted.
        /// </summary>
        public bool IsRestricted
        {
            get;
            internal set;
        }

        /// <summary>
        /// Whether or not the user is an IRC operator.
        /// </summary>
        public bool IsOperator
        {
            get;
            internal set;
        }

        /// <summary>
        /// The user can receive server notices. (Obsolete in RFC2812)
        /// </summary>
        public bool IsReceivingServerNotices
        {
            get;
            internal set;
        }

        /// <summary>
        /// The list of channels that the user is in.
        /// </summary>
        public readonly List<Channel> Channels = new List<Channel>();

        /// <summary>
        /// A dictionary that contains the current ranks for the user in each of their channels.
        /// </summary>
        public readonly Dictionary<string, UserRank> Rank = new Dictionary<string, UserRank>(StringComparer.InvariantCultureIgnoreCase);

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

        public Messages.Send.InviteMessage Invite(Channel channel)
        {
            return channel.Invite(this);
        }

        public delegate void OnHostNameChangeHandler(User user, string original);
        public event OnHostNameChangeHandler OnHostNameChange;

        internal void TriggerOnHostNameChange(string original)
        {
            if (OnHostNameChange != null)
            {
                OnHostNameChange(this, original);
            }
        }

        public delegate void OnNickNameChangeHandler(User user, string original);
        public event OnNickNameChangeHandler OnNickNameChange;

        internal void TriggerOnNickNameChange(string original)
        {
            if (OnNickNameChange != null)
            {
                OnNickNameChange(this, original);
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

        public delegate void OnUserNameChangeHandler(User user, string original);
        public event OnUserNameChangeHandler OnUserNameChange;

        internal void TriggerOnUserNameChange(string original)
        {
            if (OnUserNameChange != null)
            {
                OnUserNameChange(this, original);
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
