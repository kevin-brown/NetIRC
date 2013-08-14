using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetIRC;

namespace NetIRC.Tests
{
    [TestClass]
    public class ServerTests
    {
        [TestMethod]
        public void MotdTypeTest()
        {
            Server server = new Server("test.com", 1234);

            Messages.Send.MotdMessage motd = server.Motd();

            Assert.IsInstanceOfType(motd, typeof(Messages.Send.MotdMessage));
        }

        [TestMethod]
        public void InitSetsPropertiesTest()
        {
            Server server = new Server("test.com", 1234);

            Assert.AreEqual("test.com", server.HostName);
            Assert.AreEqual(1234, server.Port);
        }
    }
}
