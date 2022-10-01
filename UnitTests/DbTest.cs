using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class DbTest
    {
        [TestMethod]
        public void ConnectionOpenTest() =>
            Assert.IsTrue(ClientApp.Services.DatabaseHandler.ConnectionOpen(), 
                "Connection to memory failed");
    }
}
