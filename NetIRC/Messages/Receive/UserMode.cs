using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class UserMode : IReceiveMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "MODE" &&
                   !message.IsChannel();
        }

        public void ProcessMessage(ParsedMessage message, Client client)
        {
            User target = message.GetUserFromNick(message.Parameters[0]);

            if (target == client.User)
            {
                string modes = message.Parameters[1];

                ParseModes(client.User, modes);
                client.TriggerOnUserMode(modes);
            }
        }

        public void ParseModes(User user, string modes)
        {
            bool addMode = true;
            foreach (char mode in modes)
            {
                switch (mode)
                {
                    case '+': 
                        addMode = true;
                        break;
                    case '-': 
                        addMode = false;
                        break;
                    case 'a':
                        user.IsAway = addMode;
                        break;
                    case 'i':
                        user.IsInvisible = addMode;
                        break;
                    case 's':
                        user.IsReceivingServerNotices = addMode;
                        break;
                    case 'w':
                        user.IsReceivingWallOps = addMode;
                        break;
                    case 'r':
                        user.IsRestricted = addMode;
                        break;
                    case 'o':
                        user.IsOperator = addMode;
                        break;
                }
            }
        }
    }
}
