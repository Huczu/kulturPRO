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
using Database;
using Database.Services;
using kulturPRO.ViewModels;
using AutoMapper;

namespace kulturPRO
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeText();
            //throw new Exception();
        }

        private async Task InitializeText()
        {
            var userSerivce = new UserService();
            var userViewModel = Mapper.Map<UserViewModel>(await userSerivce.GetUserById(1));

            testTextBox.Text = userViewModel.FullName;
        }
    }
}
