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
        ObservableCollection<StatisticsItem> _statisticsItems;

        public static ObservableCollection<StatisticsItem> UpdateStatistics(string fileName)
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source={fileName}; Version=3;");

            // Выясняем колво строк
            int count = DatabaseHandler.SelectQuery(ref connection, "statistics", "id").Count;

            ObservableCollection<StatisticsItem> list = new ObservableCollection<StatisticsItem>();

            for (int i = 0; i < count; i++)
            {
                string name = DatabaseHandler.SelectQuery(ref connection, "statistics", "name", $"id == {i}")[0].ToString();
                int sum = (int)DatabaseHandler.SelectQuery(ref connection, "statistics", "name", $"id == {i}")[0];
                StatisticsItem item = new StatisticsItem(name, sum);
                list.Add(item);
            }



            return list;
        }
    }
}
