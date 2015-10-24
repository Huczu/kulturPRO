using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KulturPRO.ViewModels
{
    public class OnListViewModel
    {
        public ICommand ExampleCommand {get; set;}
        public void ExampleMethod()
        {
            Utillities.WindowAccessMethods.SwitchView(new Views.ExampleView());
        }

        public FunctionalList ExampleFunctionalList
        {
            get;
            set;
        }

        public OnListViewModel()
        {
            List<Function> listFunction = new List<Function>();

            ExampleCommand = new RelayCommand(o => ExampleMethod());

            Function function = new Function("FirstFunctionExample", ExampleCommand);
            listFunction.Add(function);

            ExampleFunctionalList = new FunctionalList("ExampleHeader", listFunction);
        }

    }
}
