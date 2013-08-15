using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetIRC.Tests
{
    [TestClass]
    public class ChannelTests
    {
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
    }
}
