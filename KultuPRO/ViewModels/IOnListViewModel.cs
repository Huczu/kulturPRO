using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KulturPRO.ViewModels
{
    public interface IOnListViewModel
    {
        ICommand SwitchViewCommand {get; set;}
        void SwitchView();

        FunctionalList FunctionalList
        {
            get;
            set;
        }

    }
}
