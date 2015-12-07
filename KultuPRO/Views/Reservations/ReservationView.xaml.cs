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
        private readonly ReservationsListViewModel _parent;
        private readonly ReservationViewModel _reservationViewModel;

        //for new reservation creation
        public ReservationView(ReservationsListViewModel parent, long eventId)
        {
            InitializeComponent();
            _reservationViewModel = new ReservationViewModel(eventId);
            DataContext = _reservationViewModel;
            _parent = parent;

            RegisterEvents();
        }

        //for viewing/editing existing reservation
        public ReservationView(ReservationsListViewModel parent, long eventId, long reservationId)
        {
            InitializeComponent();
            _reservationViewModel = new ReservationViewModel(eventId);
            DataContext = _reservationViewModel;
            _parent = parent;

            RegisterEvents();
        }

        private void RegisterEvents()
        {
            ButtonSave.Click += Button_Save_OnClick;
        }

        public void Button_Save_OnClick(object sender, RoutedEventArgs e)
        {
            _reservationViewModel.PostToAdd();
            _parent.Reservations.Add(_reservationViewModel.Reservation);
            this.Close();
        }
    }
}
