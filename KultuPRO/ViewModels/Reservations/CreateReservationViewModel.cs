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
    public class CreateReservationViewModel : INotifyPropertyChanged
    {
        private readonly ReservationService _reservationService = new ReservationService();

        private readonly EventService _eventService = new EventService();

        private string _personFullName;

        public string PersonFullName
        {
            get { return _personFullName; }
            set
            {
                _personFullName = value;
                OnPropertyChanged("PersonFullName");
            }
        }

        private string _contactNumber;

        public string ContactNumber
        {
            get
            {
                return _contactNumber;
            }
            set
            {
                _contactNumber = value;
                OnPropertyChanged("ContactNumber");
            }
        }

        private Event _event;

        public Event Event
        {
            get { return _event; }
            set 
            { 
                _event = value;
                OnPropertyChanged("Event");
            }
        }

        public long ReservationId { get; set; }

        public CreateReservationViewModel(long eventId)
        {
            Event = _eventService.GetEventById(eventId).Result;
        }

        public CreateReservationViewModel(long eventId, long reservationId)
        {
            Event = _eventService.GetEventById(eventId).Result;
            var reservation = _reservationService.GetReservationById(reservationId).Result;

            ReservationId = reservationId;
            PersonFullName = reservation.PersonFullName;
            ContactNumber = reservation.PhoneNumber;
        }

        public async Task<long> PostToAddNewReservation()
        {
            Reservation reservation = new Reservation
            {
                EventId = Event.Id,
                PersonFullName = PersonFullName,
                PhoneNumber = ContactNumber
            };

             return await _reservationService.AddReservationForEventId(reservation);
        }

        public async void PostToUpdateReservation()
        {
            Reservation reservation = new Reservation
            {
                EventId = Event.Id,
                Id = ReservationId,
                PersonFullName = PersonFullName,
                PhoneNumber = ContactNumber
            };

            await _reservationService.UpdateReservation(reservation);
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
