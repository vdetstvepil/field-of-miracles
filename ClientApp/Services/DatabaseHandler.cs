using ClientApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace ClientApp.Services
{
    public class DatabaseHandler
    {
        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        public SQLiteConnection Connection { get; set; }


        /// <summary>
        /// Инициировать подключение к базе данных
        /// </summary>
        /// <param name="path">Путь к файлу БД</param>
        /// <returns></returns>
        private static bool ConnectionOpen(ref SQLiteConnection connection)
        {
            connection.Open();

            if (connection.State == ConnectionState.Open)
                return true;
            else return false;
        }

        /// <summary>
        /// Создание базы данных в файле
        /// </summary>
        /// <returns></returns>
        public static bool CreateDBFile(string fileName)
        {
            // Проверяем, создан ли файл с БД
            if (File.Exists($"{fileName}"))
            {
                File.Delete($"{fileName}");
            } 
            
            // Создаем файл базы данных (формата .db)
            // в указанной директории
            SQLiteConnection.CreateFile($@"{fileName}");
            
            // Проверяем успешное создание файла
            if (File.Exists($"{fileName}"))
                return true;
            else return false;
        }

        /// <summary>
        /// Закрыть соединение с базой данных
        /// </summary>
        private static void ConnectionClose(ref SQLiteConnection connection)
        {
            if (connection != null)
                connection.Close();
        }
        
        /// <summary>
        /// Выполнение очереди запросов из файла
        /// </summary>
        /// <param name="query">Файл запросов</param>
        public static bool RunQueryFromFile(ref SQLiteConnection connection, string path)
        {
            ConnectionOpen(ref connection);

            // Читаем файл и разбиваем на отдельные запросы
            string[] query = File.ReadAllText(path).Split(
                new string[] { "\r\n", "\r", "\n" }, 
                StringSplitOptions.None);

            // Выполняем каждый запрос отдельно
            SQLiteCommand command;
            foreach (string line in query)
            {
                if (line.Contains("---"))
                    continue;
                try
                {
                    command = new SQLiteCommand(line, connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    ConnectionClose(ref connection);
                    return false;
                }
            }
            ConnectionClose(ref connection);

            return true;
        }

        /// <summary>
        /// Выполнение запроса
        /// </summary>
        /// <param name="query">Строка единичного запроса</param>
        public static bool RunQuery(ref SQLiteConnection connection, string query)
        {
            return false;
        }

        /// <summary>
        /// Запрос SELECT
        /// </summary>
        /// <param name="query">Строка единичного запроса</param>
        /// <returns></returns>
        public static List<object> SelectQuery(ref SQLiteConnection connection, string table, string field, params string[] condition)
        {
            ConnectionOpen(ref connection);

            // Строим строку запроса
            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.Append($"SELECT {field} FROM {table}");
            if (condition.Length != 0)
            {
                commandBuilder.Append(" WHERE ");
                foreach (string conditionValue in condition)
                    commandBuilder.Append(conditionValue).Append(" AND ");
                commandBuilder.Remove(commandBuilder.Length - 4, 3);
            }

            // Команда запроса
            SQLiteCommand command = new SQLiteCommand(commandBuilder.ToString(), connection);

            // Объявляем переменную чтения возвращаемого
            // потока данных (ридер)
            SQLiteDataReader reader;

            // Выполняем запрос и заливаем весь возвращаемый
            // поток данных в ридер
            reader = command.ExecuteReader();

            // Список возвращаемых величин
            List<object> listOfValues = new List<object>();

            // Вытягиваем данные из ридера до тех пор,
            // пока ридер не останется пустым
            while (reader.Read()) 
            {
                // В квадратных скобках пишем названия полей
                // таблицы таким же образом, как они указаны в запросе
                listOfValues.Add(reader[$"{field}"]);
            }
            reader.Close(); //закрываем ридер

            ConnectionClose(ref connection);

            return listOfValues;
        }
    }
}
