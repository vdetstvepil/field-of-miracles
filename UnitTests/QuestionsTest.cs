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

        private readonly List<Question> questionList = new List<Question>() {
                new Question(1, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(2, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(3, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(4, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(5, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(6, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(7, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(8, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(9, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(10, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(11, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(12, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(13, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(14, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                }),
                new Question(15, "Тестовый вопрос", new List<Variant>()
                {
                    new Variant(ClientApp.Model.VariantLetter.A, "Ответ A", true),
                    new Variant(ClientApp.Model.VariantLetter.B, "Ответ B"),
                    new Variant(ClientApp.Model.VariantLetter.C, "Ответ C"),
                    new Variant(ClientApp.Model.VariantLetter.D, "Ответ D")
                })
            };

        private List<Question> ActualQuestionList;

        public QuestionsTest()
        {
            ClientApp.Services.DatabaseHandler.CreateDBFile(fileName);

            SQLiteConnection connection = new SQLiteConnection($"Data Source={fileName}; Version=3;");
            ClientApp.Services.DatabaseHandler.RunQueryFromFile(ref connection,
                Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"/Resources/test-init.sql");

            ActualQuestionList = GamePageViewModel.GenerateQuestionList(fileName);
        }

        [TestMethod]
        public void GenerateQuestionListContentTest()
        {
            bool equality = true;
            for (int i = 0; i < 15; i++)
            {
                if (questionList[i].Content != ActualQuestionList[i].Content)
                {
                    equality = false;
                    break;
                }
                if (questionList[i].Variants[0].Content != ActualQuestionList[i].Variants[0].Content ||
                   questionList[i].Variants[1].Content != ActualQuestionList[i].Variants[1].Content ||
                   questionList[i].Variants[2].Content != ActualQuestionList[i].Variants[2].Content ||
                   questionList[i].Variants[3].Content != ActualQuestionList[i].Variants[3].Content)
                {
                    equality = false;
                    break;
                }
            }
            Assert.IsTrue(equality, "Collection equaility failed");
        }
    }
}