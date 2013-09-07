using System.IO;

namespace NetIRC.Messages.Send
{
    public class Nick : ISendMessage
    {
        public string NickName { get; set; }

        public Nick(string nickName)
        {
            this.NickName = nickName;
        }

        public Nick(User user)
        {
            this.NickName = user.NickName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("NICK " + this.NickName);
        }
    }
}
