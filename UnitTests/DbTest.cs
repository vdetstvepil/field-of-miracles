using ClientApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;

namespace UnitTests
{
    [TestClass]
    public class DbTest
    {
        private const string fileName = "test.db";

        [TestMethod]
        public void CreateDBFileTest()
        {
            
            bool result = ClientApp.Services.DatabaseHandler.CreateDBFile(fileName);

            Assert.IsTrue(result, "Test DB file not created");
        }

        [TestMethod]
        public void RunQueryTest()
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source={fileName}; Version=3;");

            bool result = ClientApp.Services.DatabaseHandler.RunQueryFromFile(ref connection,
                Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"/Resources/test-init.sql");

            Assert.IsTrue(result, "Test query failed to run");
        }

        [TestMethod]
        public void SelectQueryTest1()
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source={fileName}; Version=3;");

            string result = Convert.ToString(
                ClientApp.Services.DatabaseHandler.SelectQuery(ref connection, 
                @"SELECT question_text WHERE id == 1;"));

            Assert.AreEqual("Тестовый вопрос", result);
        }

        [TestMethod]
        public void SelectQueryTest2()
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source={fileName}; Version=3;");

            string result = Convert.ToString(
                ClientApp.Services.DatabaseHandler.SelectQuery(ref connection,
                @"SELECT variant_A WHERE id == 1;"));

            Assert.AreEqual("Ответ A", result);
        }
    }
}
