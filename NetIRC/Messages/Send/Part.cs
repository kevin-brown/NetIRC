using System.IO;

namespace NetIRC.Messages.Send
{
    public class Part : ISendMessage
    {
        private readonly string _channelName;
        private readonly string _message;

        public Part(string channelName)
        {
            this._channelName = channelName;
        }

        public Part(Channel channel)
        {
            this._channelName = channel.FullName;
        }

        public Part(string channelName, string message)
        {
            this._channelName = channelName;
            this._message = message;
        }

        public Part(Channel channel, string message)
        {
            this._channelName = channel.FullName;
            this._message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (this._message == null)
            {
                writer.WriteLine("PART {0}", this._channelName);
            }
            else
            {
                writer.WriteLine("PART {0} :{1}", this._channelName, this._message);
            }
        }
    }
}
