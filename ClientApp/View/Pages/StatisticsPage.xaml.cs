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

namespace ClientApp.View.Pages
{
    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        private Frame _frame;

        public StatisticsPage(Frame frame)
        {
            InitializeComponent();

            // Присваиваем ссылку на ViewModel DataContext'у
            DataContext = new StatisticsPageViewModel();

            _frame = frame;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            _frame.NavigationService.GoBack();
        }
    }
}
