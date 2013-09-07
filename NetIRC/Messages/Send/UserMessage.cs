using System.IO;

namespace NetIRC.Messages.Send
{
    public class UserMessage : ISendMessage
    {
        public string UserName { get; set; }
        public string RealName { get; set; }

        public UserMessage(string userName, string realName)
        {
            this.UserName = userName;
            this.RealName = realName;
        }

        public UserMessage(User user)
        {
            this.UserName = user.UserName;
            this.RealName = user.RealName;
        }

        public void Send(StreamWriter writer, Client client)
        {
            //TODO: allow mode to be set
            writer.WriteLine("USER " + this.UserName + " 0 - :" + this.RealName);
        }
    }
}
