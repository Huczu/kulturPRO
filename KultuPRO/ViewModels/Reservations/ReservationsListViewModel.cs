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
    public class ReservationsListViewModel : IOnListViewModel
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

        private long _selectedEvent;
        public long SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }
            set
            {
                _selectedEvent = value;
                UpdateReservationsList(value);
            }
        }

        public ReservationsListViewModel()
        {
            Events = eventService.GetEventsAfterDate(DateTime.Today).Result.ToList();
            SelectedEvent = 0;
            Reservations = reservationService.GetReservationsForEventId(1).Result.ToList();

            //init Commands
            SwitchViewCommand = new RelayCommand(r => SwitchView());

            //init FunctionalList with sub-functionalities
            FunctionalList = new FunctionalList("Rezerwacje", new List<Function>
            {
                new Function("Lista rezerwacji", SwitchViewCommand)
            });
        }

        public ReservationsListViewModel(long eventId)
        {
            Events.Add(eventService.GetEventById(eventId).Result);
            SelectedEvent = 0;
            Reservations = reservationService.GetReservationsForEventId(eventId).Result.ToList();

            //init Commands
            SwitchViewCommand = new RelayCommand(r => SwitchView());

            //init FunctionalList with sub-functionalities
            FunctionalList = new FunctionalList("Rezerwacje", new List<Function>
            {
                new Function("Lista rezerwacji", SwitchViewCommand)
            });
        }

        private async void UpdateReservationsList(long eventId)
        {
            var newList = await reservationService.GetReservationsForEventId(_events.ElementAt(Convert.ToInt32(eventId)).Id);
            Reservations = newList.ToList();
        }

        public FunctionalList FunctionalList { get; set; }

        public ICommand SwitchViewCommand { get; set; }

        public void SwitchView()
        {
            Utillities.WindowAccessMethods.SwitchView(new Views.Reservations.ReservationsList());
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
