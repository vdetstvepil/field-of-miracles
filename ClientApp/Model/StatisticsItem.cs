using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Model
{
    public class StatisticsItem : ViewModel
    {
        private string _name;
        private string _sum;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get => _name; }
        /// <summary>
        /// Количество очков пользователя
        /// </summary>
        public string Sum { get => _sum; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="sum">Количество очков</param>
        public StatisticsItem(string name, int sum)
        {
            _name = name;
            _sum = sum.ToString();
        }
    }


}
