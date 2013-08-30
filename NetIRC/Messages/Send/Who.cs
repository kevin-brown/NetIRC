using System.IO;

namespace NetIRC.Messages.Send
{
    public class Who : ISendMessage
    {
        string target;

        public Who(string target)
        {
            this.target = target;
        }

        public Who(Channel channel)
        {
            this.target = channel.FullName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("WHO " + this.target);
        }
    }
}
