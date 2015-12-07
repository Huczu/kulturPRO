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
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    public partial class ReservationView : Window
    {
        public ReservationView(ReservationsListViewModel parent, long eventId)
        {
            InitializeComponent();
            DataContext = new ReservationViewModel(eventId);
        }

        public ReservationView(ReservationsListViewModel parent, long eventId, long reservationId)
        {
            InitializeComponent();
            DataContext = new ReservationViewModel(eventId);
        }
    }
}
