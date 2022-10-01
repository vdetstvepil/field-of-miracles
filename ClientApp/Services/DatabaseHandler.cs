using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    internal class DatabaseHandler
    {
        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        public static SQLiteConnection Connection { get; set; }
        
        /// <summary>
        /// Инициировать подключение к базе данных
        /// </summary>
        public static void ConnectionOpen()
        {
            Connection = new SQLiteConnection($"Data Source=:memory:; Version=3;");
            Connection.Open();
        }

        /// <summary>
        /// Закрыть соединение с базой данных
        /// </summary>
        public static void ConnectionClose()
        {
            if (Connection != null)
                Connection.Close();
        }
        
        /// <summary>
        /// Выполнение запроса
        /// </summary>
        /// <param name="query">Файл запросов</param>
        public static void RunQueryFromFile(string path)
        {
            
        }

        /// <summary>
        /// Выполнение запроса
        /// </summary>
        /// <param name="query">Строка единичного запроса</param>
        public static void RunQuery(string query)
        {
            
        }
    }
}
