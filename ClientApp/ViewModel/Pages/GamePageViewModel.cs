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
        private int _score = 0;
        private int _currentLevel = 0;
        private bool _rightToMakeMistake = true;
        private bool _fiftyFifty = true;
        private int _fireproofAmount = 0;
        private string _question = "";
        private int _trueVariantNumber = 0;
        private ObservableCollection<Level> _levelItems = new ObservableCollection<Level>()
        {
            new Level(15, 3000000),
            new Level(14, 1500000),
            new Level(13, 800000),
            new Level(12, 400000),
            new Level(11, 200000),
            new Level(10, 100000),
            new Level(9, 50000),
            new Level(8, 25000),
            new Level(7, 15000),
            new Level(6, 10000),
            new Level(5, 5000),
            new Level(4, 3000),
            new Level(3, 2000),
            new Level(2, 1000),
            new Level(1, 500),
        };
        private ObservableCollection<Variant> _variantItems = new ObservableCollection<Variant>()
        {
           new Variant('A', "Ответ А"),
           new Variant('B', "Ответ B"),
           new Variant('C', "Ответ C"),
           new Variant('D', "Ответ D", true),
        };



        // Список вариантов ответа
        public List<string> AnswerVariantItems { get; set; }

        // Список уровней
        public ObservableCollection<Level> LevelItems { get => _levelItems; set => _levelItems = value; }

        // Список уровней
        public ObservableCollection<Variant> VariantItems { get => _variantItems; set => _variantItems = value; }

        // Правильный ответ
        public int TrueVariantNumber { get => _trueVariantNumber; set => _trueVariantNumber = value; }

        // Текущий вопрос
        public string Question { get => _question; set => _question = value; }

        // Количество очков
        public int Score { get => _score; set => _score = value; }

        // Текущая ступень
        public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }

        // Право на ошибку
        public bool RightToMakeMistake { get => _rightToMakeMistake; set => _rightToMakeMistake = value; }

        // 50:50
        public bool FiftyFifty { get => _fiftyFifty; set => _fiftyFifty = value; }

        // Несгораемая сумма
        public int FireproofAmount { get => _fireproofAmount; set => _fireproofAmount = value; }
    }
}
