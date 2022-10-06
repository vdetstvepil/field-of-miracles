using ClientApp.Model;
using ClientApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClientApp.ViewModel.Pages
{
    public class GamePageViewModel : Model.ViewModel
    {
        // Делегаты
        public delegate void StartGameDelegate(int fireproofLevel);

        // Локальные переменные
        private int _score;
        private int _currentLevel;
        private bool _fiftyFifty;
        private int _fireproofLevel;
        private string _question;
        private int _trueVariantNumber;
        private Visibility _endGameControlVisibility;
        private string _nickname;
        private ObservableCollection<Level> _levelItems;
        private ObservableCollection<Variant> _variantItems;
        List<Question> _questions;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="nickname"></param>
        public GamePageViewModel(string nickname)
        {
            // Инициализация делегатов
            StartGameDelegate startGameDelegate = StartGame;

            // Инициализация переменных
            _score = 0;
            _currentLevel = 0;
            _rightToMakeMistake = true;
            _fiftyFifty = true;
            _fireproofLevel = 0;
            _question = "Выберите на правой панели несгораемую сумму";
            _trueVariantNumber = 0;
            _endGameControlVisibility = Visibility.Collapsed;
            _nickname = nickname;
            _levelItems = new ObservableCollection<Level>()
            {
                new Level(15, 3000000, startGameDelegate),
                new Level(14, 1500000, startGameDelegate),
                new Level(13, 800000, startGameDelegate),
                new Level(12, 400000, startGameDelegate),
                new Level(11, 200000, startGameDelegate),
                new Level(10, 100000, startGameDelegate),
                new Level(9, 50000, startGameDelegate),
                new Level(8, 25000, startGameDelegate),
                new Level(7, 15000, startGameDelegate),
                new Level(6, 10000, startGameDelegate),
                new Level(5, 5000, startGameDelegate),
                new Level(4, 3000, startGameDelegate),
                new Level(3, 2000, startGameDelegate),
                new Level(2, 1000, startGameDelegate),
                new Level(1, 500, startGameDelegate),
            };
            _variantItems = new ObservableCollection<Variant>();
            _questions = new List<Question>();
        }

        /// <summary>
        /// Генерирование списка вопросов для текущей игры
        /// </summary>
        /// <returns></returns>
        public static List<Question> GenerateQuestionList(string fileName)
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source={fileName}; Version=3;");

            // Рандомайзер
            Random random = new Random();

            // Список вопросов
            List<Question> questions = new List<Question>();

            for (int i = 1; i <= 15; i++)
            {
                // Берем рандомный вопрос по уровню и добавляем его в список
                List<object> questionContentItems =
                    DatabaseHandler.SelectQuery(ref connection, "questions", "id", $"level_number == {i}");
                int questionId = Convert.ToInt32(questionContentItems[random.Next(0, questionContentItems.Count)]);
                string temp = Convert.ToString(DatabaseHandler.SelectQuery(
                                ref connection, "questions", "variant_true", $"id == {questionId}")[0]);
                questions.Add(
                    new Question(
                    i,
                    Convert.ToString(DatabaseHandler.SelectQuery(
                        ref connection, "questions", "question_text", $"id == {questionId}")[0]),
                    new List<Variant>()
                    {
                        new Variant(
                            VariantLetter.A,
                            Convert.ToString(DatabaseHandler.SelectQuery(
                                ref connection, "questions", "variant_a", $"id == {questionId}")[0]),
                            Convert.ToString(DatabaseHandler.SelectQuery(
                                ref connection, "questions", "variant_true", $"id == {questionId}")[0]) == "A"
                                ? true : false),
                        new Variant(
                            VariantLetter.B,
                            Convert.ToString(DatabaseHandler.SelectQuery(
                                ref connection, "questions", "variant_b", $"id == {questionId}")[0]),
                            Convert.ToString(DatabaseHandler.SelectQuery(
                                ref connection, "questions", "variant_true", $"id == {questionId}")[0]) == "B"
                                ? true : false),
                        new Variant(
                            VariantLetter.C,
                            Convert.ToString(DatabaseHandler.SelectQuery(
                                ref connection, "questions", "variant_c", $"id == {questionId}")[0]),
                            Convert.ToString(DatabaseHandler.SelectQuery(
                                ref connection, "questions", "variant_true", $"id == {questionId}")[0]) == "C"
                                ? true : false),
                        new Variant(
                            VariantLetter.D,
                            Convert.ToString(DatabaseHandler.SelectQuery(
                                ref connection, "questions", "variant_d", $"id == {questionId}")[0]),
                            Convert.ToString(DatabaseHandler.SelectQuery(
                                ref connection, "questions", "variant_true", $"id == {questionId}")[0]) == "D"
                                ? true : false),
                    })
                );
            }

            return questions;
        }

        /// <summary>
        /// Сохранение результата в базу данных
        /// </summary>
        private void SaveResult()
        {
            // Создаем подключение к базе данных
            SQLiteConnection connection = new SQLiteConnection($"Data Source={DatabaseHandler.DatabaseFileName}; Version=3;");

            // Выполняем запрос вставки значений в статистику
            DatabaseHandler.InsertQuery(ref connection, "statistics_table", 
                new string[] { "nickname", "score" }, 
                new string[] { NickName, Score.ToString() });
        }

        /// <summary>
        /// Начать игру
        /// </summary>
        /// <param name="fireproofLevel"></param>
        private void StartGame(int fireproofLevel)
        {
            // Получаем на вход уровень, на котором будет
            // получена несгораемая сумма
            FireproofLevel = fireproofLevel;

            // Генерируем лист вопросов
            Questions = GenerateQuestionList(DatabaseHandler.DatabaseFileName);

            // Текущий уровень - первый
            CurrentLevel = 1;

            // Выключаем возможность нажатия на уровни
            foreach (Level level in LevelItems)
                level.IsEnabled = false;
        }

        /// <summary>
        /// Выбрать вариант ответа
        /// </summary>
        /// <param name="letter">Выбранный вариант</param>
        public void ChooseVariant(VariantLetter letter)
        {
            // Выделение нажатого варианта ответа
            foreach (var item in _variantItems)
                if (item.Letter == letter)
                    item.IsChecked = true;
                else item.IsChecked = false;
        }

        /// <summary>
        /// Окончательно ответить на вопрос
        /// </summary>
        public void AnswerQuestion()
        {
            foreach (Variant item in VariantItems)
                if (item.IsChecked == true)
                {
                    if (item.IsTrue)
                        CurrentLevel++;
                    else
                    {
                        // Если уровень несгораемой суммы еще не был
                        // пройдет, то выигрыш будет нулевым. Иначе
                        // выигрыш будет равняться несгораемой сумме.
                        if (CurrentLevel <= FireproofLevel)
                            Score = 0;
                        else foreach (Level level in LevelItems)
                                if (level.IsFireproof)
                                    Score = Int32.Parse(level.Sum);

                        // Показываем экран окончания игры с результатом
                        EndGameControlVisibility = Visibility.Visible;

                        // Сохраняем результат
                        SaveResult();
                    }
                    break;
                }
        }

        private void LoadQuestion()
        {
            // Выводим вопрос на экран
            Question = Questions[CurrentLevel - 1].Content;
            OnPropertyChanged("Question");

            // Выводим варианты
            VariantItems.Clear();
            foreach (Variant item in Questions[CurrentLevel - 1].Variants)
                VariantItems.Add(item);

            // Выделяем текущий уровень
            foreach (Level item in LevelItems)
                item.IsCurrent = false;
            LevelItems[15 - CurrentLevel].IsCurrent = true;
            OnPropertyChanged("ItemColor");

            // Показываем текущую сумму
            if (15 - CurrentLevel + 1 < 15)
                Score = Convert.ToInt32(LevelItems[15 - CurrentLevel + 1].Sum);
            else Score = 0;
           
        }

        /// <summary>
        /// Активировать подсказку 50:50
        /// </summary>
        public void RemoveTwoVariants()
        {
            foreach (Variant item in VariantItems)
                item.IsChecked = false;

            foreach (Variant item in VariantItems)
                if (item.IsTrue)
                {
                    // Запоминаем правильный вариант
                    Variant temp = item;
                    VariantItems.Remove(temp);

                    // Убираем первый неверный вариант
                    Variant tempRemove1 = VariantItems[new Random().Next(0, 3)];
                    tempRemove1.IsVisible = Visibility.Hidden;
                    VariantItems.Remove(tempRemove1);

                    // Убираем второй неверный вариант
                    Variant tempRemove2 = VariantItems[new Random().Next(0, 2)];
                    tempRemove2.IsVisible = Visibility.Hidden;
                    VariantItems.Remove(tempRemove2);

                    // Возвращаем варианты
                    VariantItems.Add(temp);
                    VariantItems.Add(tempRemove1);
                    VariantItems.Add(tempRemove2);

                    // Деактивируем опцию 50:50
                    FiftyFifty = false;
                    break;
                }
        }
       

        /// <summary>
        /// Список вариантов ответа
        /// </summary>
        public List<string> AnswerVariantItems { get; set; }

        /// <summary>
        /// Список уровней
        /// </summary>
        public ObservableCollection<Level> LevelItems { get => _levelItems; set => _levelItems = value; }

        /// <summary>
        /// Список уровней
        /// </summary>
        public ObservableCollection<Variant> VariantItems { get => _variantItems; set => _variantItems = value; }

        /// <summary>
        /// Правильный ответ
        /// </summary>
        public int TrueVariantNumber { get => _trueVariantNumber; set => _trueVariantNumber = value; }

        /// <summary>
        /// Текущий вопрос
        /// </summary>
        public string Question { get => _question; set => _question = value; }

        /// <summary>
        /// Количество очков
        /// </summary>
        public int Score 
        { 
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        /// <summary>
        /// Текущая ступень
        /// </summary>
        public int CurrentLevel 
        { 
            get => _currentLevel; 
            set
            {
                if (value >= 1 && value <= 15)
                {
                    _currentLevel = value;
                    LoadQuestion();
                }
                if (value == 16)
                {
                    _currentLevel++;

                    // Показываем экран окончания игры с результатом
                    EndGameControlVisibility = Visibility.Visible;

                    // Сохраняем результат
                    SaveResult();
                }
            }  
        }

        // 50:50
        public bool FiftyFifty 
        { 
            get => _fiftyFifty; 
            set
            {
                _fiftyFifty = value;
                OnPropertyChanged("FiftyFifty");
            }
        }

        /// <summary>
        /// Несгораемая сумма
        /// </summary>
        public int FireproofLevel { get => _fireproofLevel; set => _fireproofLevel = value; }

        /// <summary>
        /// Список вопросов
        /// </summary>
        public List<Question> Questions { get => _questions; set => _questions = value; }

        /// <summary>
        /// Видимость _endGameControl
        /// </summary>
        public Visibility EndGameControlVisibility
        {
            get => _endGameControlVisibility; set
            {
                _endGameControlVisibility = value;
                OnPropertyChanged("EndGameControlVisibility");
            }
        }

        /// <summary>
        /// Никнейм
        /// </summary>
        public string NickName
        {
            get => _nickname;
            set => _nickname = value;
        }
    }
}
