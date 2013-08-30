using System;
using System.Linq;

namespace NetIRC.Messages.Receive.Numerics
{
    class WelcomeMessage : ReceiveNumericMessage
    {
        public static bool CheckMessage(ParsedMessage message, Client client)
        {
            return message.Command == "001";
        }

        public override void ProcessMessage(ParsedMessage message, Client client)
        {
            User target = message.GetUserFromNick(message.Parameters[0]);

            if (target == client.User)
            {
                string welcome = message.Parameters[1];

                client.TriggerOnConnect();
                client.TriggerOnWelcome(welcome);
            }
        }
    }
}
