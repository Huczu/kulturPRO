using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Database.Services;

namespace KulturPRO.ViewModels.Reservations
{
    public class SeatReservationViewModel : INotifyPropertyChanged
    {
        private readonly ReservationService _reservationService = new ReservationService();

        private SeatReservation _seatReservation;

        public SeatReservation SeatReservation
        {
            get { return _seatReservation; }
            set
            {
                _seatReservation = value;
                OnPropertyChanged("SeatReservation");
            }
        }

        public SeatReservationViewModel(long id)
        {
            SeatReservation = _reservationService.GetSeatReservationById(id).Result;
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
