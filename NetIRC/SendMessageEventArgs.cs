using NetIRC.Messages;

namespace NetIRC
{
    public class SendMessageEventArgs
    {
        public ISendMessage Message { get; set; }

        public SendMessageEventArgs(ISendMessage message)
        {
            this.Message = message;
        }
    }
}
