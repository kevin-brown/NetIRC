﻿using System;

namespace NetIRC
{
    public class ChannelTopic
    {
        public User Author
        {
            get;
            internal set;
        }

        public string Message
        {
            get;
            internal set;
        }

        public DateTime LastUpdated
        {
            get;
            internal set;
        }

        internal void ClearTopic()
        {
            this.Message = null;
            this.Author = null;
            this.LastUpdated = DateTime.Now;
        }

        public override string ToString()
        {
            if (this.Author != null)
            {
                return string.Format("\"{0}\" set by {1} on {2}", this.Message, this.Author.NickName, this.LastUpdated);
            }
            else
            {
                return "";
            }
        }
    }
}
