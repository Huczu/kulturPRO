using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KulturPRO.ViewModels
{
    class CinemHallsOnListViewModel : IOnListViewModel
    {
    /// <summary>
    /// viewmodel for stackpanel on right side
    /// </summary>
        public FunctionalList FunctionalList { get; set; }

        public ICommand SwitchViewCommand { get; set; }

        public ICommand OpenWindowCommand { get; set; }

        public void SwitchView()
        {
            Utillities.WindowAccessMethods.SwitchView(new Views.generating());
        }

        public void OpenWindow()
        {
            Views.Halls halls = new Views.Halls();
            halls.Show();
        }

        public CinemHallsOnListViewModel()
        {
            SwitchViewCommand = new RelayCommand(o => SwitchView());
            OpenWindowCommand = new RelayCommand(o => OpenWindow());
            FunctionalList = new FunctionalList("Sale kinowe", new List<Function>
        {
            new Function("Sale", SwitchViewCommand),
            new Function("Dodaj salę",OpenWindowCommand)
        });
        }
    }
}
