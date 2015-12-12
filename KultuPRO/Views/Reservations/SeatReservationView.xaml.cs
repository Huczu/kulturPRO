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
using KulturPRO.ViewModels.Reservations;

namespace KulturPRO.Views.Reservations
{
    /// <summary>
    /// Interaction logic for SeatReservationView.xaml
    /// </summary>
    public partial class SeatReservationView : Window
    {
        private SeatReservationViewModel _context;

        public SeatReservationView()
        {
            InitializeComponent();
        }

        public SeatReservationView(long seatReservationId)
        {
            InitializeComponent();
            _context = new SeatReservationViewModel(seatReservationId);
            DataContext = _context;

            HallContent.Content = new generating(new List<Seat>
            {
                _context.SeatReservation.Seat
            }, 
            _context.SeatReservation.Seat.CinemaHallId,
            false);
        }
    }
}
