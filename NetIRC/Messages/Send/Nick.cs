using System.IO;

namespace NetIRC.Messages.Send
{
    public class Nick : ISendMessage
    {
        private readonly string _nickName;

        public Nick(string nickName)
        {
            this._nickName = nickName;
        }

        public Nick(User user)
        {
            this._nickName = user.NickName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            writer.WriteLine("NICK " + this._nickName);
        }
    }
}
