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
using System.Windows.Shapes;

namespace KulturPRO.Views
{
    /// <summary>
    
    /// Interaction logic for gen2.xaml
    /// </summary>
    public partial class gen2 : Window
    {
        public gen2()
        {
            InitializeComponent();
        }

        public string Contents { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }

    }


        
}
