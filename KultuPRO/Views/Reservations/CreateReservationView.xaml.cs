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
using KulturPRO.ViewModels.Reservations;

namespace KulturPRO.Views.Reservations
{
    /// <summary>
    /// Interaction logic for CreateReservationView.xaml
    /// </summary>
    public partial class CreateReservationView : Window
    {
        private readonly CreateReservationViewModel _createReservationViewModel;

        private readonly ReservationsList _reservationsList;

        public CreateReservationView(long eventId, ReservationsList reservationsList)
        {
            _createReservationViewModel = new CreateReservationViewModel(eventId);
            _reservationsList = reservationsList;

            InitializeComponent();
            DataContext = _createReservationViewModel;
        }

        public CreateReservationView(long eventId, ReservationsList reservationsList, long reservationId)
        {
            _createReservationViewModel = new CreateReservationViewModel(eventId, reservationId);
            _reservationsList = reservationsList;

            InitializeComponent();
            DataContext = _createReservationViewModel;
        }

        public async void Save_Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_createReservationViewModel.ReservationId.Equals(0))
            {
                _createReservationViewModel.PostToUpdateReservation();
                this.Close();

                var reservationView = new ReservationView(_reservationsList, _createReservationViewModel.Event.Id, _createReservationViewModel.ReservationId);
                reservationView.Show();
            }
            else
            {
                var createdId = await _createReservationViewModel.PostToAddNewReservation();
                this.Close();

                var reservationView = new ReservationView(_reservationsList, _createReservationViewModel.Event.Id, createdId);
                reservationView.Show();
            }
        }
    }
}
