using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString()
        {
            if (this.Author != null)
            {
                return string.Format("{0} set by {1} on {2}", this.Message, this.Author.NickName, this.LastUpdated);
            }
            else
            {
                return "";
            }
        }
    }
}
