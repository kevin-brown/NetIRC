using System.IO;

namespace NetIRC.Messages.Send
{
    public class Part : ISendMessage
    {
        public string ChannelName { get; set; }
        public string Message { get; set; }

        public Part(string channelName)
        {
            this.ChannelName = channelName;
        }

        public Part(Channel channel)
        {
            this.ChannelName = channel.FullName;
        }

        public Part(string channelName, string message)
        {
            this.ChannelName = channelName;
            this.Message = message;
        }

        public Part(Channel channel, string message)
        {
            this.ChannelName = channel.FullName;
            this.Message = message;
        }

        public void Send(StreamWriter writer, Client client)
        {
            if (this.Message == null)
            {
                writer.WriteLine("PART {0}", this.ChannelName);
            }
            else
            {
                writer.WriteLine("PART {0} :{1}", this.ChannelName, this.Message);
            }
        }
    }
}
