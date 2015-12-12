using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Database.Models;
using Database.Services;
using KulturPRO.Views.Reservations;

namespace KulturPRO.ViewModels.Reservations
{
    public class ReservationViewModel : INotifyPropertyChanged
    {
        private readonly ReservationService _reservationService = new ReservationService();

        private long _eventId;

        private Reservation _reservation;

        public Reservation Reservation
        {
            set { _reservation = value; }
            get { return _reservation; }
        }

        private ObservableCollection<SeatReservation> _seatReservations;

        public ObservableCollection<SeatReservation> SeatReservations
        {
            set { _seatReservations = value; }
            get { return _seatReservations; }
        }

        public List<Seat> GetSeatsReserved()
        {
            return SeatReservations.Select(s => s.Seat).ToList();
        }

        public ReservationViewModel(long eventId)
        {
            _eventId = eventId;
            Reservation = new Reservation
            {
                EventId = eventId
            };
            AddNewSeatCommand = new RelayCommand(r => AddNewSeat());
        }

        public ReservationViewModel(long eventId, long reservationId)
        {
            _eventId = eventId;
            Reservation = _reservationService.GetReservationById(reservationId).Result;
            SeatReservations = new ObservableCollection<SeatReservation>(Reservation.SeatReservations);
            AddNewSeatCommand = new RelayCommand(r => AddNewSeat());
        }

        public void PostToAdd()
        {
            return;
        }

        public ICommand AddNewSeatCommand { get; set; }
        public async void AddNewSeat()
        {
            if (await _reservationService.CanReserveForEvent(Reservation.EventId))
            {
                SeatReservationView seatReservationView = new SeatReservationView(Reservation,
                    _reservationService.GetSeatReservationsForReservationId(Reservation.Id).Result.Select(r => r.Seat).ToList());
                seatReservationView.Closed += (sender, args) =>
                {
                    UpdateView();
                };
                seatReservationView.Show();
            }
            else
            {
                MessageBox.Show("Brak wolnych miejsc");
            }
        }

        public async void UpdateView()
        {
            Reservation = await _reservationService.GetReservationById(Reservation.Id);
            SeatReservations = new ObservableCollection<SeatReservation>(Reservation.SeatReservations);
            OnPropertyChanged("SeatReservations");
            OnPropertyChanged("Reservation");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
