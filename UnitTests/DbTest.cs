using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Versioning;

namespace UnitTests
{
    [TestClass]
    public class DbTest
    {
        [TestMethod]
        public void ConnectionOpenTest() =>
            Assert.IsTrue(ClientApp.Services.DatabaseHandler.ConnectionOpen(), 
                "Connection to memory failed");

        [TestMethod]
        public void RunTestQuery() =>
            Assert.IsTrue(ClientApp.Services.DatabaseHandler.RunQueryFromFile(@"Resources/test-init.sql"),
                "Test query failed to run");
    }
}
