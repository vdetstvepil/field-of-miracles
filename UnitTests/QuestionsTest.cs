using ClientApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using ClientApp.Model;
using ClientApp.ViewModel.Pages;

namespace UnitTests
{
    [TestClass]
    public class QuestionsTest
    {
        private const string fileName = "test2.db";

        [TestMethod]
        public void GenerateQuestionListTest()
        {
            ClientApp.Services.DatabaseHandler.CreateDBFile(fileName);

            SQLiteConnection connection = new SQLiteConnection($"Data Source={fileName}; Version=3;");
            ClientApp.Services.DatabaseHandler.RunQueryFromFile(ref connection,
                Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"/Resources/test-init.sql");

            CollectionAssert.AreEqual(new List<Question>() { 
                new Question(1, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                })
            }, (new GamePageViewModel()).GenerateQuestionList(fileName), "Collection equaility failed");
        }

    }
}