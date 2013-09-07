using System.IO;

namespace NetIRC.Messages.Send
{
    public class Who : ISendMessage
    {
        public string Target { get; set; }

        public Who(string target)
        {
            this.Target = target;
        }

        public Who(Channel channel)
        {
            this.Target = channel.FullName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("WHO " + this.Target);
        }
    }
}
