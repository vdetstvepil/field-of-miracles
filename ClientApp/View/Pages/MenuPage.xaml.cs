using ClientApp.View.Pages;
using ClientApp.ViewModel.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private Frame _frame;

        public MenuPage(Frame frame)
        {
            InitializeComponent();
            DataContext = new MenuPageViewModel();
            _frame = frame;
        }

        // Выход из приложения
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MenuPageViewModel)DataContext).InitGameControlVisibility = Visibility.Visible;
        }

        private void StatisticsBtn_Click(object sender, RoutedEventArgs e)
        {
            _frame.NavigationService.Navigate(new StatisticsPage(_frame));
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            _frame.NavigationService.Navigate(new GamePage(_frame));
        }
    }
}
