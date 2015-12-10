using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Database.Services;

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
        }

        public ReservationViewModel(long eventId, long reservationId)
        {
            _eventId = eventId;
            Reservation = _reservationService.GetReservationById(reservationId).Result;
            SeatReservations = new ObservableCollection<SeatReservation>( _reservationService.GetSeatReservationsForReservationId(reservationId).Result );
        }

        public void PostToAdd()
        {
            return;
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
