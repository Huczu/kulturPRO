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
using Database.Models;
using KulturPRO.ViewModels.Reservations;

namespace KulturPRO.Views.Reservations
{
    /// <summary>
    /// Interaction logic for ReservationsList.xaml
    /// </summary>
    public partial class ReservationsList : UserControl
    {
        public ReservationsList()
        {
            InitializeComponent();
        }

        public void UpdateReservationList(object sender, SelectionChangedEventArgs e)
        {
            if(dgReservations!=null)
            {
                dgReservations.GetBindingExpression(DataGrid.ItemsSourceProperty).UpdateTarget();
            }
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

            if (row != null)
            {
                var item = row.Item as Reservation;

                ReservationView reservationDetails = new ReservationView(DataContext as ReservationsListViewModel, item.EventId, item.Id);
                reservationDetails.Show();
            }
        }
    }
}
