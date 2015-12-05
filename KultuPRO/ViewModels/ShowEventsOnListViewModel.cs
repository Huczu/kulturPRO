using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KulturPRO.ViewModels
{
    /// <summary>
    /// viewmodel for stackpanel on right side
    /// </summary>
    class ShowEventsOnListViewModel : IOnListViewModel
    {
        public FunctionalList FunctionalList { get; set; }

        public ICommand SwitchViewCommand { get; set; }

        public void SwitchView()
        {
            Utillities.WindowAccessMethods.SwitchView(new Views.ShowEventsControl());
        }

        public ICommand OpenAddEventViewCommand { get; set; }
        public void OpenAddEvent()
        {
            Views.AddEventView aev = new Views.AddEventView();
            aev.Show();
        }

        public ShowEventsOnListViewModel()
        {
            SwitchViewCommand = new RelayCommand(o => SwitchView());
            OpenAddEventViewCommand = new RelayCommand(o => OpenAddEvent());

            FunctionalList = new FunctionalList("Wydarzenia", new List<Function>
            {
                new Function("Pokaż wydarzenia", SwitchViewCommand),
                new Function("Dodaj wydarzenie",OpenAddEventViewCommand)
            });
        }
    }
}
