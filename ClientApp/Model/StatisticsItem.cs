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

        public string Name { get => _name; }
        public string Sum { get => _sum; }

        public StatisticsItem(string name, int sum)
        {
            _name = name;
            _sum = sum.ToString();
        }
    }


}
