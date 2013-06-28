using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class UserModeMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            string[] parts = message.Split(' ');

            return ReceiveUserMessage.CheckCommand(message, "MODE") &&
                parts[2].StartsWith("#") == false; //it's not a channel
        }

        public override void ProcessMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            string modes = parts[3];

            ParseModes(client.User, modes);

            client.TriggerOnUserMode(modes);
        }

        public void ParseModes(User user, string modes)
        {
            bool addMode = true;
            for (int i = 0; i < modes.Length; i++)
            {
                switch (modes[i])
                {
                    case '+': addMode = true;
                        break;
                    case '-': addMode = false;
                        break;
                    case 'a':
                        if (addMode)
                            user.Away = true;
                        else
                            user.Away = false;
                        break;
                    case 'i':
                        if (addMode)
                            user.Invisible = true;
                        else
                            user.Invisible = false;
                        break;
                    case 'w':
                        if (addMode)
                            user.WallOps = true;
                        else
                            user.WallOps = false;
                        break;
                    case 'r':
                        if (addMode)
                            user.Restricted = true;
                        else
                            user.Restricted = false;
                        break;
                    case 'o':
                        if (addMode)
                            user.Operator = true;
                        else
                            user.Operator = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
