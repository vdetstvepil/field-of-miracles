using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp.ViewModel.Pages
{
    public class MenuPageViewModel : Model.ViewModel
    {
        private Visibility _initGameControlVisibility = Visibility.Collapsed;

        public Visibility InitGameControlVisibility
        {
            get { return _initGameControlVisibility; }
            set 
            {
                _initGameControlVisibility = value;
                OnPropertyChanged("InitGameControlVisibility");
            }
        }

        public MenuPageViewModel()
        {

        }
    }
}
