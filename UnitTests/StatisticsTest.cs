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
using System.Collections.ObjectModel;

namespace UnitTests
{
    [TestClass]
    public class StatisticsTest
    {
        private const string fileName = "test3.db";

        private readonly ObservableCollection<StatisticsItem> StatisticsItems 
            = new ObservableCollection<StatisticsItem>() {
            new StatisticsItem("Dashka", 3000000),
            new StatisticsItem("Margo", 3000000),
            new StatisticsItem("Zhenek", 1500000),
            new StatisticsItem("Anton", 5000),
            };

        private ObservableCollection<StatisticsItem> ActualStatisticsItems;

        public StatisticsTest()
        {
            ClientApp.Services.DatabaseHandler.CreateDBFile(fileName);

            SQLiteConnection connection = new SQLiteConnection($"Data Source={fileName}; Version=3;");
            ClientApp.Services.DatabaseHandler.RunQueryFromFile(ref connection,
                Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"/Resources/test-init.sql");

            ActualStatisticsItems = StatisticsPageViewModel.UpdateStatistics(fileName);
        }

        [TestMethod]
        public void LoadStatisticsTest() => 
            Assert.IsTrue(
                ActualStatisticsItems[0].Name == StatisticsItems[0].Name &&
                ActualStatisticsItems[0].Sum == StatisticsItems[0].Sum &&
                ActualStatisticsItems[1].Name == StatisticsItems[1].Name &&
                ActualStatisticsItems[1].Sum == StatisticsItems[1].Sum &&
                ActualStatisticsItems[2].Name == StatisticsItems[2].Name &&
                ActualStatisticsItems[2].Sum == StatisticsItems[2].Sum &&
                ActualStatisticsItems[2].Name == StatisticsItems[2].Name &&
                ActualStatisticsItems[3].Sum == StatisticsItems[3].Sum, 
                "Collection equaility failed");
    }
}