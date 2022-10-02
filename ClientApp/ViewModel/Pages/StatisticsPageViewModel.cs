using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApp.Model;
using ClientApp.Services;

namespace ClientApp.ViewModel.Pages
{
    public class StatisticsPageViewModel : Model.ViewModel
    {
        private ObservableCollection<StatisticsItem> _statisticsItems;

        /// <summary>
        /// Список элементов с именами и количеством очков
        /// </summary>
        public ObservableCollection<StatisticsItem> StatisticsItems { 
            get => _statisticsItems; set => _statisticsItems = value; }

        /// <summary>
        /// Выгружает статистику из базы данных
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Отсортированный по убыванию список с количеством очков и именами</returns>
        public static ObservableCollection<StatisticsItem> UpdateStatistics(string fileName)
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source={fileName}; Version=3;");

            // Количество строк в базе данных
            int count = DatabaseHandler.SelectQuery(ref connection, "statistics", "id").Count;

            // Список всех элементов
            ObservableCollection<StatisticsItem> list 
                = new ObservableCollection<StatisticsItem>();

            // Выгрузка статистики из базы данных
            for (int i = 0; i < count; i++)
            {
                string name = DatabaseHandler.SelectQuery(ref connection,
                    "statistics_table", "nickname", $"id == {i}")[0].ToString();
                int sum = (int)DatabaseHandler.SelectQuery(ref connection, 
                    "statistics_table", "score", $"id == {i}")[0];
                list.Add(new StatisticsItem(name, sum));
            }

            // Сортировка по убыванию по количеству очков
            return (ObservableCollection<StatisticsItem>)list.OrderByDescending(
                p => Convert.ToInt32(p.Sum));
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public StatisticsPageViewModel()
        {
            StatisticsItems = UpdateStatistics("questions.db");
        }
    }
}
