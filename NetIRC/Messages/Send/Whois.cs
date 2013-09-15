using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NetIRC.Messages.Send
{
    class Whois
    {
        public string Nick { get; set; }
        public string Server { get; set; }

        public Whois(string nick)
        {
            this.Nick = nick;
        }

        public Whois(User user)
        {
            this.Nick = user.NickName;
        }

        public Whois(string nick, string server)
        {
            this.Nick = nick;
            this.Server = server;
        }

        public Whois(User user, string server)
        {
            this.Server = server;
            this.Nick = user.NickName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (String.IsNullOrEmpty(this.Nick))
            {
                writer.WriteLine("WHOIS {0}", this.Nick);
            }
            else
            {
                writer.WriteLine("WHOIS {1} {0}", this.Nick, this.Server);
            }
        }
    }
}
