using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ClientApp.Model
{
    public class Level : ViewModel
    {
        private string _number;
        private string _sum;
        private bool _isFireProof = false;
        private bool _isCurrent = false;

        /// <summary>
        /// Порядковый номер уровня
        /// </summary>
        public string Number { get { return _number; } }

        /// <summary>
        /// Количество очков на данном уровне
        /// </summary>
        public string Sum { get { return _sum; } }  

        /// <summary>
        /// Несгораемая сумма
        /// </summary>
        public bool IsFireproof { 
            get => _isFireProof; 
            set
            {
                _isFireProof = value;
                OnPropertyChanged("ItemColor");
            }
        }

        /// <summary>
        /// Текущий уровень
        /// </summary>
        public bool IsCurrent { 
            get => _isCurrent; 
            set
            {
                _isCurrent = value;
                OnPropertyChanged("ItemColor");
            }
        }

        /// <summary>
        /// Цвет текста
        /// </summary>
        public Brush ItemColor { 
            get
            {
                if (IsFireproof)
                    return (Brush)new BrushConverter().ConvertFrom("#FFFFFFFF");
                else if (IsCurrent)
                    return (Brush)new BrushConverter().ConvertFrom("#FF5BFF6C");
                else 
                    return (Brush)new BrushConverter().ConvertFrom("#FFFF8B5B");
            } 
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="number">Порядковый номер уровня</param>
        /// <param name="sum">Количество очков на данном уровне</param>
        public Level(int number, int sum)
        {
            _number = number.ToString();
            _sum = sum.ToString();
        }
    }
}
