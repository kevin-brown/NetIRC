using System.IO;

namespace NetIRC.Messages.Send
{
    public class Part : ISendMessage
    {
        private string channelName;
        private string message;

        public Part(string channelName)
        {
            this.channelName = channelName;
        }

        public Part(Channel channel)
        {
            this.channelName = channel.FullName;
        }

        public Part(string channelName, string message)
        {
            this.channelName = channelName;
            this.message = message;
        }

        public Part(Channel channel, string message)
        {
            this.channelName = channel.FullName;
            this.message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (this.message == null)
            {
                writer.WriteLine("PART {0}", this.channelName);
            }
            else
            {
                writer.WriteLine("PART {0} :{1}", this.channelName, this.message);
            }
        }
    }
}
