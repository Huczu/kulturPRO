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
using KulturPRO.Views.Reservations;

namespace KulturPRO.ViewModels.Reservations
{
    public class ReservationsListViewModel : IOnListViewModel
    {
        private readonly ReservationService _reservationService = new ReservationService();
        private readonly EventService _eventService = new EventService();

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

        private ObservableCollection<Reservation> _reservations;
        public ObservableCollection<Reservation> Reservations
        {
            get
            {
                return _reservations;
            }
            set
            {
                _reservations = value;
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
                OnPropertyChanged("SelectedEvent");
            }
        }

        public ReservationsListViewModel()
        {
            Events = _eventService.GetEventsAfterDate(DateTime.Today).Result.ToList();
            SelectedEvent = 0;
            Reservations = new ObservableCollection<Reservation>(_reservationService.GetReservationsForEventId(1).Result);

            //init Commands
            SwitchViewCommand = new RelayCommand(r => SwitchView());
            AddNewViewCommand = new RelayCommand(r => AddNewView());

            //init FunctionalList with sub-functionalities
            FunctionalList = new FunctionalList("Rezerwacje", new List<Function>
            {
                new Function("Lista rezerwacji", SwitchViewCommand)
            });
        }

        public ReservationsListViewModel(long eventId)
        {
            Events.Add(_eventService.GetEventById(eventId).Result);
            SelectedEvent = 0;
            Reservations = new ObservableCollection<Reservation>(_reservationService.GetReservationsForEventId(eventId).Result);

            //init Commands
            SwitchViewCommand = new RelayCommand(r => SwitchView());
            AddNewViewCommand = new RelayCommand(r => AddNewView());

            //init FunctionalList with sub-functionalities
            FunctionalList = new FunctionalList("Rezerwacje", new List<Function>
            {
                new Function("Lista rezerwacji", SwitchViewCommand)
            });
        }

        private async void UpdateReservationsList(long eventId)
        {
            var list = await _reservationService.GetReservationsForEventId(_events.ElementAt(Convert.ToInt32(eventId)).Id);
            Reservations = new ObservableCollection<Reservation>(list);
        }

        public FunctionalList FunctionalList { get; set; }

        public ICommand SwitchViewCommand { get; set; }

        public ICommand AddNewViewCommand { get; set; }

        public void SwitchView()
        {
            Utillities.WindowAccessMethods.SwitchView(new Views.Reservations.ReservationsList());
        }

        public async void AddNewView()
        {
            if (await _reservationService.CanReserveForEvent(_events.ElementAt(Convert.ToInt32(_selectedEvent)).Id))
            {
                ReservationView reservationView = new ReservationView(this, _events.ElementAt(Convert.ToInt32(_selectedEvent)).Id);
                reservationView.Show();
            }
            else
            {
                MessageBox.Show("Brak dostępnych miejsc");
            }

            UpdateReservationsList(_selectedEvent);
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
