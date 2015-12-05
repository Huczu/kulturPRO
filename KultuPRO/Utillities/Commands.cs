using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace KulturPRO.Utillities
{
    /// <summary>
    /// komenda zamknięcia okna
    /// </summary>
    class ExitCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return parameter != null;
        }

        void ICommand.Execute(object parameter)
        {
            MessageBoxResult result = new MessageBoxResult();

            result = MessageBox.Show("Czy na pewno chcesz zamknąć okno?", "Zamykanie okna", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ((Window)parameter).Close();
            }
        }
    }

    /// <summary>
    /// komenda logowania
    /// </summary>
    class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter)
        {
            Database.Models.User user = new Database.Models.User();
            return parameter != null;
        }

        public void Execute(object parameter)
        {

            //TODO: logging implementation

            if(!WindowAccessMethods.IsWindowExists<Views.MainWindow>())
            {
                Views.MainWindow mainWindow = new Views.MainWindow();
                mainWindow.Show();
            }
        }
    }


    /// <summary>
    /// komenda wyświetlania ustawień
    /// </summary>
    class ShowSettingsCommand : ICommand
    {

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if(WindowAccessMethods.MainContent==null)
            {
                WindowAccessMethods.MainContent = new Views.SettingsControl();
            }
            else if(WindowAccessMethods.MainContent.GetType()!= typeof(Views.SettingsControl))
            {
                WindowAccessMethods.MainContent.Content = new Views.SettingsControl();
            }     
        }
    }
}
