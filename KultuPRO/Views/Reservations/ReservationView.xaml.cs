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
using Database.Models;
using Database.Services;
using KulturPRO.ViewModels.Reservations;
using KulturPRO.Views;
using MenuItem = System.Windows.Forms.MenuItem;

namespace KulturPRO.Views.Reservations
{
    /// <summary>
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    public partial class ReservationView : Window
    {
        private readonly ReservationsListViewModel _parent;
        private readonly ReservationViewModel _reservationViewModel;
        private readonly ReservationService _reservationService = new ReservationService();

        //for new reservation creation
        public ReservationView(ReservationsListViewModel parent, long eventId)
        {
            InitializeComponent();
            _reservationViewModel = new ReservationViewModel(eventId);
            DataContext = _reservationViewModel;
            _parent = parent;

            Title = "Nowa rezerwacja";
            RegisterEvents();
        }

        //for viewing/editing existing reservation
        public ReservationView(ReservationsListViewModel parent, long eventId, long reservationId)
        {
            InitializeComponent();
            _reservationViewModel = new ReservationViewModel(eventId, reservationId);
            DataContext = _reservationViewModel;
            _parent = parent;

            ReservationHall.Content = new generating(_reservationViewModel.GetSeatsReserved(), _reservationViewModel.Reservation.Event.CinemaHallId, false);

            Title = "Rezerwacja nr " + reservationId;
            RegisterEvents();
        }



        private void RegisterEvents()
        {
            ButtonSave.Click += Button_Save_OnClick;
        }

        public async void Button_AddSeat_OnClick(object senderr, RoutedEventArgs e)
        {
            if (await _reservationService.CanReserveForEvent(_reservationViewModel.Reservation.EventId))
            {
                SeatReservationView seatReservationView = new SeatReservationView(_reservationViewModel.Reservation,
                    _reservationService.GetSeatReservationsForReservationId(_reservationViewModel.Reservation.Id).Result.Select(r => r.Seat).ToList());
                seatReservationView.Closed += (sender, args) =>
                {
                    _reservationViewModel.UpdateView();
                    ReservationHall.Content = new generating(_reservationViewModel.GetSeatsReserved(), _reservationViewModel.Reservation.Event.CinemaHallId, false);
                };
                seatReservationView.Show();
            }
            else
            {
                MessageBox.Show("Brak wolnych miejsc");
            }
        }

        public async void Grid_ContextMenu_Delete_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.MenuItem row = sender as System.Windows.Controls.MenuItem;

            if (row != null)
            {
                var contextMenu = row.CommandParameter as ContextMenu;

                if (contextMenu != null)
                {
                    var grid = contextMenu.PlacementTarget as DataGrid;
                    var toDeleteFromBindedList = (SeatReservation)grid.SelectedCells[0].Item;
                    if (await _reservationService.DeleteSeatReservation(toDeleteFromBindedList))
                    {
                        _reservationViewModel.UpdateView();
                        ReservationHall.Content = new generating(_reservationViewModel.GetSeatsReserved(), _reservationViewModel.Reservation.Event.CinemaHallId, false);
                    }
                }
            }
        }

        public void Grid_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

            if (row != null)
            {
                var item = row.Item as SeatReservation;

                SeatReservationView seatReservationDetails = new SeatReservationView(item.Id, item.ReservationId);
                seatReservationDetails.Show();
            }
        }

        public void Button_Save_OnClick(object sender, RoutedEventArgs e)
        {
            _reservationViewModel.PostToAdd();
            _parent.Reservations.Add(_reservationViewModel.Reservation);
            this.Close();
        }

        public T GetVisualParent<T>(object childObject) where T : Visual
        {
            DependencyObject child = childObject as DependencyObject;
            while ((child != null) && !(child is T))
            {
                child = VisualTreeHelper.GetParent(child);
            }
            return child as T;
        }
    }
}
