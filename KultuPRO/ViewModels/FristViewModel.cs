using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KulturPRO.ViewModels
{
    //tworzycie klasę dziedziczącą po OnListViewModel

    public class FirstViewModel : OnListViewModel
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

        //dodajecie właściwość z klasy FunctionalList
        public FunctionalList FunctionalList
        {
            get;
            set;
        }

        //tworzymy konstruktor
        public FirstViewModel()
        {
            //tworzymy listę funkcji (metod z podpisami przycisków)
            List<Function> listFunction = new List<Function>();

            //w taki sposób komendę przypisujemy do danej metody, jeżeli mamy taką potrzebę, można dodać po przecinku warunek możliwości wciśnięcia kontrolki wykonującej metodę (tutaj metoda Condition),
            //jeżeli wpiszemy ExampleCommand = new RelayCommand(o => ExampleMethod()); będzie można komendę wykonać bez żadnego warunku
            ExampleCommand = new RelayCommand(o => ExampleMethod(),o => Condition());

            //łączymy komendę z napisem na przycisku
            Function function = new Function("Przykład", ExampleCommand);

            //każdą Function dodajemy do listy funkcji
            listFunction.Add(function);

            //tworzymy sobie nową functional list dopisując nagłówek listy
            ExampleFunctionalList = new FunctionalList("Nagłówek", listFunction);
        }

        //teraz przechodzimy do MainWindow.xaml.cs
    }
}
