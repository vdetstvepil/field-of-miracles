using ClientApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModel.Pages
{
    internal class GamePageViewModel
    {
        public int _score = 0;
        public int _level = 0;
        public bool _rightToMakeMistake = true;
        public bool _fiftyFifty = true;
        public int _fireproofAmount = 0;
        public string _question = "";
        public int _trueVariantNumber = 0;

        // Список вариантов ответа
        public List<string> AnswerVariantItems { get; set; }

        // Правильный ответ
        public int TrueVariantNumber { get => _trueVariantNumber; set => _trueVariantNumber = value; }

        // Вопрос
        public string Question { get => _question; set => _question = value; }

        // Количество очков
        public int Score { get => _score; set => _score = value; }

        // Текущая ступень
        public int Level { get => _level; set => _level = value; }

        // Право на ошибку
        public bool RightToMakeMistake { get => _rightToMakeMistake; set => _rightToMakeMistake = value; }

        // 50:50
        public bool FiftyFifty { get => _fiftyFifty; set => _fiftyFifty = value; }

        // Несгораемая сумма
        public int FireproofAmount { get => _fireproofAmount; set => _fireproofAmount = value; }
    }
}
