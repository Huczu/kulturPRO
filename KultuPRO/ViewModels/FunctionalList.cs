using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KulturPRO.ViewModels
{
    public class FunctionalList
    {
        public string Header
        {
            get;
            set;
        }

        public List<Function> MethodsList
        {
            get;
            set;
        }

        public FunctionalList() {}
        public FunctionalList(string header,List<Function> methodsList)
        {
            Header = header;
            MethodsList = methodsList;
        }
    }

    public class Function
    {
        public string Name
        {
            get;
            set;
        }

        public ICommand Command
        {
            get;
            set;
        }

        public Function(string name, ICommand command)
        {
            Name = name;
            Command = command;
        }
    }
}
