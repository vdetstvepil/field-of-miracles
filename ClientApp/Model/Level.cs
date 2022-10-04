using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using static ClientApp.ViewModel.Pages.GamePageViewModel;
using System.Windows.Navigation;

namespace ClientApp.Model
{
    public class Level : ViewModel
    {
        private string _number;
        private string _sum;
        private bool _isFireProof = false;
        private bool _isCurrent = false;
        private StartGameDelegate _startGame;
        private bool _isEnabled = true;

        /// <summary>
        /// Порядковый номер уровня
        /// </summary>
        public string Number { get { return _number; } }

        /// <summary>
        /// Количество очков на данном уровне
        /// </summary>
        public string Sum { get { return _sum; } }  

        /// <summary>
        /// Кликабельность
        /// </summary>
        public bool IsEnabled 
        { 
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
            }
        }

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
                    return (Brush)new BrushConverter().ConvertFrom("#FF5BFF6C"); 
                else if (IsCurrent)
                    return (Brush)new BrushConverter().ConvertFrom("#FFFFFFFF");
                else 
                    return (Brush)new BrushConverter().ConvertFrom("#FFFF8B5B");
            } 
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="number">Порядковый номер уровня</param>
        /// <param name="sum">Количество очков на данном уровне</param>
        public Level(int number, int sum, StartGameDelegate startGameDelegate)
        {
            _number = number.ToString();
            _sum = sum.ToString();

            LevelClick = new LambdaCommand(OnLevelClickExecuted, CanLevelClickExecute);
            _startGame = startGameDelegate;
        }

        /// <summary>
        /// Команда нажатия левой кнопкой мыши
        /// </summary>
        public ICommand LevelClick { get; set; }

        /// <summary>
        /// Проверочное условие на возможность нажатия
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool CanLevelClickExecute(object p) => _isEnabled;

        /// <summary>
        /// Действие при нажатии на элемент
        /// </summary>
        /// <param name="p"></param>
        private void OnLevelClickExecuted(object p)
        {
            IsFireproof = true;
            _startGame.Invoke(Int32.Parse(Number));
        }
    }
}
