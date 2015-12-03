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

namespace KulturPRO.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //używacie metody AddNewItem, by stworzyć nowy View dla swojego ViewModelu listy po prawej stronie,
            AddNewItem(new ViewModels.FirstViewModel());
            AddNewItem(new ViewModels.CinemHallsOnListViewModel());
            AddNewItem(new ViewModels.AdminViewModel());
        }

        private void brTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        //to Was nie interesuje
        private void AddNewItem(ViewModels.IOnListViewModel exampleOnListViewModel)
        {
            Expander expander = new Expander();
            expander.HorizontalAlignment = HorizontalAlignment.Stretch;
            expander.VerticalAlignment = VerticalAlignment.Stretch;
            expander.Header = exampleOnListViewModel.FunctionalList.Header;

            StackPanel stackpanel = new StackPanel();
            stackpanel.VerticalAlignment = VerticalAlignment.Stretch;
            stackpanel.HorizontalAlignment = HorizontalAlignment.Stretch;
      
            foreach(var function in exampleOnListViewModel.FunctionalList.MethodsList)
            {
                Button button = new Button();
                button.HorizontalAlignment = HorizontalAlignment.Stretch;
                button.VerticalAlignment = VerticalAlignment.Stretch;
                button.Content = function.Name;
                button.Command = function.Command;
                button.Style = (Style)FindResource("ButtonStyle");

                stackpanel.Children.Add(button);
            }

            expander.Content = stackpanel;

            spFunctionality.Children.Add(expander);

        }
    }
}
