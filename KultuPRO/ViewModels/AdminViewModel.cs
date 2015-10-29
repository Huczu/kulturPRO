using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KulturPRO.ViewModels
{
    public class AdminViewModel : IOnListViewModel
    {
        public AdminViewModel()
        {
            //init Commands
            SwitchViewCommand = new RelayCommand(r => SwitchView());
            SwitchAuditListViewCommand = new RelayCommand(r => SwitchAuditListView());


            //init FunctionalList with sub-functionalities
            FunctionalList = new FunctionalList("Administracja", new List<Function>
            {
                new Function("Log akcji użytkowników", SwitchAuditListViewCommand)
            });
        }

        public FunctionalList FunctionalList { get; set; }

        public ICommand SwitchViewCommand { get; set; }

        public void SwitchView()
        {
            Utillities.WindowAccessMethods.SwitchView(new Views.AdminView());
        }

        public ICommand SwitchAuditListViewCommand { get; set; }

        private void SwitchAuditListView()
        {
            Utillities.WindowAccessMethods.SwitchView(new Views.Audit.AuditListView());
        }
    }
}
