using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KulturPRO.ViewModels
{
    /// <summary>
    /// interface to implement viewmodel for panel on the right
    /// the best way to do this
    /// </summary>
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
