using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ClientApp.Model
{
    internal class Variant : ViewModel
    {
        private string _content;
        private char _letter;
        private bool _isChecked = false;
        private bool _isVisible = true;
        private bool _isTrue;
        private Brush _itemColor = null;

        /// <summary>
        /// Текст варианта ответа
        /// </summary>
        public string Content { get { return _content; } }

        /// <summary>
        /// Буква варианта
        /// </summary>
        public char Letter { get { return _letter; } }

        /// <summary>
        /// Выбранный ответ
        /// </summary>
        public bool IsChecked { get => _isChecked; set => _isChecked = value; }

        /// <summary>
        /// Видимость варианта ответа
        /// </summary>
        public bool IsVisible { get => _isVisible; set => _isVisible = value; }

        /// <summary>
        /// Правильный ответ
        /// </summary>
        public bool IsTrue { get => _isTrue; }

        /// <summary>
        /// Цвет варианта ответа
        /// </summary>
        public Brush ItemColor
        {
            get => _itemColor;
            set => _itemColor = value;
        }

        /// <summary>
        /// Настроить цвет варианта ответа
        /// </summary>
        public void SetItemColor(string color)
        {
            ItemColor = (Brush)new BrushConverter().ConvertFrom(color);
        }

        /// <summary>
        /// Номер строки в Grid'е
        /// </summary>
        public int RowNumber
        { 
            get
            {
                if (Letter == 'A' || Letter == 'B')
                    return 0;
                else if (Letter == 'C' || Letter == 'D')
                    return 1;
                else 
                    throw new Exception("Wrong letter");
            } 
        }

        /// <summary>
        /// Номер столбца в Grid'е
        /// </summary>
        public int ColumnNumber
        {
            get
            {
                if (Letter == 'A' || Letter == 'C')
                    return 0;
                else if (Letter == 'B' || Letter == 'D')
                    return 1;
                else
                    throw new Exception("Wrong letter");
            }
        }

        public Variant(char letter, string content, bool isTrue = false)
        {
            _letter = letter;
            _content = content;
            _isTrue = isTrue;
            _itemColor = (Brush)new BrushConverter().ConvertFrom("#4CFFFFFF");
        }
    }
}
