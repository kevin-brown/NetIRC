using System;

namespace NetIRC.Messages.Receive.Numerics
{
    class WelcomeMessage : ReceiveNumericMessage
    {
        public static bool CheckMessage(string message, Server server)
        {
            return ReceiveNumericMessage.CheckNumeric(message, server, 001);
        }

        public override void ProcessMessage(string message, Client client)
        {
            Console.WriteLine("RAWR");
        }
    }
}
