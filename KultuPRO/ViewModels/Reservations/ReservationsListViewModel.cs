using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Input;
using Database.Services;
using Database.Models;

namespace KulturPRO.ViewModels.Reservations
{
    public class ReservationsListViewModel : INotifyPropertyChanged
    {
        private readonly ReservationService reservationService = new ReservationService();
        private readonly EventService eventService = new EventService();

        private List<Event> _events;
        public List<Event> Events
        {
            get
            {
                return _events;
            }
            set
            {
                _events = value;
                OnPropertyChanged("Events");
            }
        }

        private List<Reservation> _reservations;
        public List<Reservation> Reservations
        {
            get
            {
                return _reservations;
            }
            set
            {
                _reservations = value;
                OnPropertyChanged("Reservations");
            }
        }

        private long _eventId;
        public long EventId
        {
            get
            {
                return _eventId;
            }
            set
            {
                _eventId = value;
                UpdateReservationsList(value);
            }
        }

        public ReservationsListViewModel()
        {
            EventId = 1;
            Events = eventService.GetEvents().Result;
            Reservations = reservationService.GetReservationsForEventId(1).Result.ToList();
        }

        public ReservationsListViewModel(long eventId)
        {
            EventId = eventId;
            Events.Add(eventService.GetEventById(eventId).Result);
            Reservations = reservationService.GetReservationsForEventId(eventId).Result.ToList();
        }

        private async void UpdateReservationsList(long eventId)
        {
            var newList = await reservationService.GetReservationsForEventId(eventId);
            Reservations = newList.ToList();
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
