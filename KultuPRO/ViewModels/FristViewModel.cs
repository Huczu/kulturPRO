using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KulturPRO.ViewModels
{
    //tworzycie klasę dziedziczącą po OnListViewModel

    public class FirstViewModel : IOnListViewModel
    {
        //dodajecie metody i do każdej metody właściwość ICommand
        public ICommand FirstCommand { get; set; }
        public void FirstMethod()
        {
            //UWAGA
            //metody SwitchView używacie, by stworzyć nową instancję widoku, który chcecie wstawić do głównej części okna!!!
            Utillities.WindowAccessMethods.SwitchView(new Views.ExampleView());

            //Wasze Views muszą być typu UserControl, no chyba, że chcecie tworzyć nowe okno, wtedy nie używacie SwitchView, tylko robicie:
            //NaszTypView windowExample = NaszTypView(); 
            //windowExample.Show();
        }
        public bool Condition()
        {
            return true;
        }

        public void SwitchView()
        {
            Utillities.WindowAccessMethods.SwitchView(new Views.ExampleView());
        }

        //dodajecie właściwość z klasy FunctionalList
        public FunctionalList FunctionalList
        {
            get;
            set;
        }

        public ICommand SwitchViewCommand
        {
            get;

            set;
        }

        //tworzymy konstruktor
        public FirstViewModel()
        {
            SwitchViewCommand = new RelayCommand(o => SwitchView());
            FunctionalList = new FunctionalList("test", new List<Function>
            {
                new Function("przycisk", SwitchViewCommand)
            });
        }


        //teraz przechodzimy do MainWindow.xaml.cs
    }
}
