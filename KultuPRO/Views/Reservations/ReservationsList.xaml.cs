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
using Database.Services;
using KulturPRO.ViewModels.Reservations;

namespace KulturPRO.Views.Reservations
{
    /// <summary>
    /// Interaction logic for ReservationsList.xaml
    /// </summary>
    public partial class ReservationsList : UserControl
    {
        public readonly ReservationsListViewModel _reservationsListViewModel = new ReservationsListViewModel();

        private readonly ReservationService _reservationService = new ReservationService();

        public ReservationsList()
        {
            InitializeComponent();
            DataContext = _reservationsListViewModel;
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

                ReservationView reservationDetails = new ReservationView(this, item.EventId, item.Id);
                reservationDetails.Closed += (o, args) =>
                {
                    _reservationsListViewModel.UpdateReservationsList(item.EventId);
                    UpdateReservationList(null, null);
                };
                reservationDetails.Show();
            }
        }

        private async void AddNew_Button_OnClick(object sender, RoutedEventArgs e)
        {
            var eventId = _reservationsListViewModel.Events.ElementAt(Convert.ToInt32(_reservationsListViewModel.SelectedEvent)).Id;

            if (await _reservationService.CanReserveForEvent(eventId))
            {
                CreateReservationView createReservationView = new CreateReservationView(eventId, this);
                createReservationView.Closed += (o, args) =>
                {
                    _reservationsListViewModel.UpdateReservationsList(eventId);
                    UpdateReservationList(null, null);
                };
                createReservationView.Show();
            }
            else
            {
                MessageBox.Show("Brak wolnych miejsc");
            }
        }
    }
}
