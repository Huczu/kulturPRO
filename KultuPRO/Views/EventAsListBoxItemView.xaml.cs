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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KulturPRO.Views
{
    /// <summary>
    /// Ta kontrolka jest wzorem itemu dla listboxa wydarzeń dla wybranej daty
    /// </summary>
    public partial class EventAsListBoxItemView : UserControl
    {
        public EventAsListBoxItemView(Database.Models.Event Event)
        {
            InitializeComponent();
            this.DataContext = Event;
        }
    }
}
