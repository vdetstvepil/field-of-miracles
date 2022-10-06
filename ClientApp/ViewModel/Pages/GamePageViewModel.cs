using ClientApp.Model;
using ClientApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int _score = 0;
        private int _currentLevel = 0;
        private bool _fiftyFifty = true;
        private int _fireproofLevel = 0;
        private string _question = "";
        private int _trueVariantNumber = 0;
        private Visibility _endGameControlVisibility = Visibility.Collapsed;

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
           new Variant(VariantLetter.A, "Ответ А"),
           new Variant(VariantLetter.B, "Ответ B"),
           new Variant(VariantLetter.C, "Ответ C"),
           new Variant(VariantLetter.D, "Ответ D", true),
        };
        List<Question> _questions = new List<Question>();


        public GamePageViewModel()
        {
            OnPropertyChanged("ItemColor");
            OnPropertyChanged("Question");

            Questions = GenerateQuestionList("questions.db");
            CurrentLevel = 1;
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
                // Берем рандомный вопрос по уровню
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
        /// Выбрать вариант ответа
        /// </summary>
        /// <param name="letter">Выбранный вариант</param>
        public void ChooseVariant(VariantLetter letter)
        {
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
                        if (CurrentLevel < FireproofLevel)
                            Score = 0;

                        // Вызов экрана конца игры
                        EndGameControlVisibility = Visibility.Visible;
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
            {
                Score = Convert.ToInt32(LevelItems[15 - CurrentLevel + 1].Sum);
            }
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
        public int Score 
        { 
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        // Текущая ступень
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
                    // Вызов экрана конца игры
                    EndGameControlVisibility = Visibility.Visible;
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

        // Несгораемая сумма
        public int FireproofLevel { get => _fireproofLevel; set => _fireproofLevel = value; }

        // Список вопросов
        public List<Question> Questions { get => _questions; set => _questions = value; }

        // Видимость _endGameControl
        public Visibility EndGameControlVisibility
        {
            get => _endGameControlVisibility; set
            {
                _endGameControlVisibility = value;
                OnPropertyChanged("EndGameControlVisibility");
            }
        }
    }
}
