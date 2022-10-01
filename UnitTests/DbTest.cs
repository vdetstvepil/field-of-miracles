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
        public void CreateDBFileTest() =>
          Assert.IsTrue(ClientApp.Services.DatabaseHandler.CreateDBFile(@"test.db"),
              "Test DB file not created");

        [TestMethod]
        public void RunQueryTest() =>
            Assert.IsTrue(ClientApp.Services.DatabaseHandler.RunQueryFromFile(@"Resources/test-init.sql"),
                "Test query failed to run");

        [TestMethod]
        public void SelectQueryTest1() =>
            Assert.AreEqual(Convert.ToString(
                ClientApp.Services.DatabaseHandler.SelectQuery(@"SELECT question_text WHERE id == 1;")),
                "Тестовый вопрос");

        [TestMethod]
        public void SelectQueryTest2() =>
            Assert.AreEqual(Convert.ToString(
                ClientApp.Services.DatabaseHandler.SelectQuery(@"SELECT variant_a WHERE id == 1;")),
                "Ответ A");

        [TestMethod]
        public void SelectQueryTest3() =>
           Assert.AreEqual(Convert.ToString(
               ClientApp.Services.DatabaseHandler.SelectQuery(@"SELECT variant_b WHERE id == 1;")),
               "Ответ B");

        [TestMethod]
        public void SelectQueryTest4() =>
         Assert.AreEqual(Convert.ToString(
             ClientApp.Services.DatabaseHandler.SelectQuery(@"SELECT variant_c WHERE id == 1;")),
             "Ответ C");

        [TestMethod]
        public void SelectQueryTest5() =>
         Assert.AreEqual(Convert.ToString(
             ClientApp.Services.DatabaseHandler.SelectQuery(@"SELECT variant_d WHERE id == 1;")),
             "Ответ D");


        [TestMethod]
        public void SelectQueryTest6() =>
         Assert.AreEqual(Convert.ToString(
             ClientApp.Services.DatabaseHandler.SelectQuery(@"SELECT variant_true WHERE id == 1;")),
             "A");
    }
}
