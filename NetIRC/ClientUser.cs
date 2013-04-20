using System;

namespace NetIRC
{
    public class ClientUser : User
    {
        public ClientUser(string nick) : base(nick) { }
        public ClientUser(string nick, string user) : base(nick, user) { }
        public ClientUser(string nick, string user, string real) : base(nick, user, real) { }
    }
}
