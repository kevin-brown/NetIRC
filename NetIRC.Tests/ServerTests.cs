using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetIRC;

namespace NetIRC.Tests
{
    [TestClass]
    public class ServerTests
    {
        [TestMethod]
        public void InitSetsPropertiesTest()
        {
            Server server = new Server("test.com", 1234);

            Assert.AreEqual("test.com", server.HostName);
            Assert.AreEqual(1234, server.Port);
        }
    }
}
