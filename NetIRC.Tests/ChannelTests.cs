using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetIRC.Tests
{
    [TestClass]
    public class ChannelTests
    {
        [TestMethod]
        public void InitWithTypeTest()
        {
            Channel channel = new Channel("test", ChannelType.Safe);

            Assert.AreEqual("test", channel.Name);
            Assert.AreEqual(ChannelType.Safe, channel.Type);
        }

        [TestMethod]
        public void InitNameNoTypeTest()
        {
            Channel channel = new Channel("test");

            Assert.AreEqual("test", channel.Name);
            Assert.AreEqual(ChannelType.Network, channel.Type);
        }

        [TestMethod]
        public void InitNameNetworkTest()
        {
            Channel channel = new Channel("#test");

            Assert.AreEqual("test", channel.Name);
            Assert.AreEqual(ChannelType.Network, channel.Type);
        }

        [TestMethod]
        public void InitNameLocalTest()
        {
            Channel channel = new Channel("&test");

            Assert.AreEqual("test", channel.Name);
            Assert.AreEqual(ChannelType.Local, channel.Type);
        }

        [TestMethod]
        public void InitNameSafeTest()
        {
            Channel channel = new Channel("!test");

            Assert.AreEqual("test", channel.Name);
            Assert.AreEqual(ChannelType.Safe, channel.Type);
        }

        [TestMethod]
        public void InitNameUnmoderatedTest()
        {
            Channel channel = new Channel("+test");

            Assert.AreEqual("test", channel.Name);
            Assert.AreEqual(ChannelType.Unmoderated, channel.Type);
        }

        [TestMethod]
        public void GetTopicTest()
        {
            Channel channel = new Channel("#channel");

            Messages.Send.TopicMessage topic = channel.GetTopic();

            string output = TestHelpers.GetSendMessageOutput(topic);

            Assert.AreEqual("TOPIC #channel", output);
        }

        [TestMethod]
        public void InviteTest()
        {
            Channel channel = new Channel("#channel");
            User user = new User("user");

            Messages.Send.InviteMessage invite = channel.Invite(user);

            string output = TestHelpers.GetSendMessageOutput(invite);

            Assert.AreEqual("INVITE user #channel", output);
        }

        [TestMethod]
        public void JoinTest()
        {
            Channel channel = new Channel("#channel");

            Messages.Send.JoinMessage join = channel.Join();

            string output = TestHelpers.GetSendMessageOutput(join);

            Assert.AreEqual("JOIN #channel", output);
        }

        [TestMethod]
        public void KickTest()
        {
            Channel channel = new Channel("#channel");
            User user = new User("user");

            Messages.Send.KickMessage kick = channel.Kick(user);

            string output = TestHelpers.GetSendMessageOutput(kick);

            Assert.AreEqual("KICK #channel user", output);
        }

        [TestMethod]
        public void KickReasonTest()
        {
            Channel channel = new Channel("#channel");
            User user = new User("user");

            Messages.Send.KickMessage kick = channel.Kick(user, "reason");

            string output = TestHelpers.GetSendMessageOutput(kick);

            Assert.AreEqual("KICK #channel user :reason", output);
        }

        [TestMethod]
        public void PartTest()
        {
            Channel channel = new Channel("#channel");

            Messages.Send.PartMessage part = channel.Part();

            string output = TestHelpers.GetSendMessageOutput(part);

            Assert.AreEqual("PART #channel", output);
        }

        [TestMethod]
        public void PartReasonTest()
        {
            Channel channel = new Channel("#channel");

            Messages.Send.PartMessage part = channel.Part("reason");

            string output = TestHelpers.GetSendMessageOutput(part);

            Assert.AreEqual("PART #channel :reason", output);
        }

        [TestMethod]
        public void SendNoticeTest()
        {
            Channel channel = new Channel("#channel");

            Messages.Send.ChannelNoticeMessage notice = channel.SendNotice("message");

            string output = TestHelpers.GetSendMessageOutput(notice);

            Assert.AreEqual("NOTICE #channel :message", output);
        }

        [TestMethod]
        public void SendMessageTest()
        {
            Channel channel = new Channel("#channel");

            Messages.Send.ChatMessage message = channel.SendMessage("message");

            string output = TestHelpers.GetSendMessageOutput(message);

            Assert.AreEqual("PRIVMSG #channel :message", output);
        }

        [TestMethod]
        public void SetTopicTest()
        {
            Channel channel = new Channel("#channel");

            Messages.Send.TopicMessage topic = channel.SetTopic("test topic");

            string output = TestHelpers.GetSendMessageOutput(topic);

            Assert.AreEqual("TOPIC #channel :test topic", output);
        }
    }
}
