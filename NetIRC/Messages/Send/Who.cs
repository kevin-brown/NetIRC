using System.IO;

namespace NetIRC.Messages.Send
{
    public class Who : ISendMessage
    {
        private readonly string _target;

        public Who(string target)
        {
            this._target = target;
        }

        public Who(Channel channel)
        {
            this._target = channel.FullName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("WHO " + this._target);
        }
    }
}
