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
using KulturPRO.ViewModels;
using KulturPRO.ViewModels.Reservations;

namespace KulturPRO.Views.Reservations
{
    /// <summary>
    /// Interaction logic for SeatReservationView.xaml
    /// </summary>
    public partial class SeatReservationView : Window
    {
        private SeatReservationViewModel _context;
        private readonly bool _isNew;

        public SeatReservationView(Reservation reservation, List<Seat> seatsAlreadyTaken)
        {
            _isNew = true;
            InitializeComponent();
            _context = new SeatReservationViewModel(reservation.Id);
            DataContext = _context;
            TypeNameAndPriceText.Visibility = Visibility.Hidden;
            HallContent.Content = new generating(seatsAlreadyTaken, reservation.Event.CinemaHallId, true, _context.SelectedSeat);
        }

        public SeatReservationView(long seatReservationId, long reservationId)
        {
            _isNew = false;
            InitializeComponent();
            _context = new SeatReservationViewModel(seatReservationId, reservationId);
            DataContext = _context;
            SaveButton.Visibility = Visibility.Hidden;

            HallContent.Content = new generating(new List<Seat>
            {
                _context.SeatReservation.Seat
            }, 
            _context.SeatReservation.Seat.CinemaHallId,
            false);
        }

        public void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            _context.PostToAddNew();
            this.Close();
        }
    }
}
