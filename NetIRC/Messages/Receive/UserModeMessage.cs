using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetIRC.Messages.Receive
{
    class UserModeMessage : ReceiveUserMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            string[] parts = message.Split(' ');

            return ReceiveUserMessage.CheckCommand(message, "MODE") &&
                Channel.TypeChars.Values.Contains(parts[2][0]) == false; //it's not a channel
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
                            user.IsAway = true;
                        else
                            user.IsAway = false;
                        break;
                    case 'i':
                        if (addMode)
                            user.IsInvisible = true;
                        else
                            user.IsInvisible = false;
                        break;
                    case 's':
                        if (addMode)
                            user.IsReceivingServerNotices = true;
                        else
                            user.IsReceivingServerNotices = false;
                        break;
                    case 'w':
                        if (addMode)
                            user.IsReceivingWallOps = true;
                        else
                            user.IsReceivingWallOps = false;
                        break;
                    case 'r':
                        if (addMode)
                            user.IsRestricted = true;
                        else
                            user.IsRestricted = false;
                        break;
                    case 'o':
                        if (addMode)
                            user.IsOperator = true;
                        else
                            user.IsOperator = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
