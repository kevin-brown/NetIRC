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

            Assert.AreEqual(motd.GetType(), typeof(Messages.Send.MotdMessage));
        }
    }
}
