using System;
using System.Linq;

namespace NetIRC.Messages.Receive.Numerics
{
    class WelcomeMessage : ReceiveNumericMessage
    {
        public static bool CheckMessage(string message, Client client)
        {
            return ReceiveNumericMessage.CheckNumeric(message, client, 001);
        }

        public override void ProcessMessage(string message, Client client)
        {
            client.TriggerOnConnect();

            string[] parts = message.Split(' ');
            string welcome = string.Join(" ", parts.Skip(3));

            client.TriggerOnWelcome(welcome);
        }
    }
}
