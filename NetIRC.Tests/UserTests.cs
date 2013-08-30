using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetIRC;

namespace NetIRC.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void InitNickTests()
        {
            User user = new User("nick");

            Assert.AreEqual("nick", user.NickName);
            Assert.AreEqual("nick", user.UserName);
            Assert.AreEqual("nick", user.RealName);
        }

        [TestMethod]
        public void InitNickUserTests()
        {
            User user = new User("nick", "user");

            Assert.AreEqual("nick", user.NickName);
            Assert.AreEqual("nick", user.RealName);
            Assert.AreEqual("user", user.UserName);
        }

        [TestMethod]
        public void InitNickUserRealTests()
        {
            User user = new User("nick", "user", "real");

            Assert.AreEqual("nick", user.NickName);
            Assert.AreEqual("user", user.UserName);
            Assert.AreEqual("real", user.RealName);
        }

        [TestMethod]
        public void InviteTests()
        {
            User user = new User("user");
            Channel channel = new Channel("channel");

            Messages.Send.Invite invite = user.Invite(channel);

            string output = TestHelpers.GetSendMessageOutput(invite);

            Assert.AreEqual("INVITE user #channel", output);
        }
    }
}
