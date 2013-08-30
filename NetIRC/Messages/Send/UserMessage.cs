using System.IO;

namespace NetIRC.Messages.Send
{
    public class UserMessage : ISendMessage
    {
        private readonly string _userName;
        private readonly string _realName;

        public UserMessage(string userName, string realName)
        {
            this._userName = userName;
            this._realName = realName;
        }

        public UserMessage(User user)
        {
            this._userName = user.UserName;
            this._realName = user.RealName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            //TODO: allow mode to be set
            writer.WriteLine("USER " + this._userName + " 0 - :" + this._realName);
        }
    }
}
