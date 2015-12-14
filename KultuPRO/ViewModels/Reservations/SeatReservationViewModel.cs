using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Database.Models;
using Database.Services;

namespace KulturPRO.ViewModels.Reservations
{
    public class SeatReservationViewModel : INotifyPropertyChanged
    {
        private readonly ReservationService _reservationService = new ReservationService();

        private readonly TicketService _ticketService = new TicketService();

        private readonly bool _isNew;

        public bool IsNew
        {
            get
            {
                return _isNew;
            }
        }

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

        private List<Ticket> _ticketTypes; 

        public List<Ticket> TicketTypes
        {
            get
            {
                return _ticketTypes;
            }
            set
            {
                _ticketTypes = value;
                OnPropertyChanged("TicketTypes");
            }
        }

        private Ticket _selectedTicket;

        public Ticket SelectedTicket
        {
            get { return _selectedTicket; }
            set
            {
                _selectedTicket = value;
                OnPropertyChanged("SelectedTicket");
            }
        }

        public Seat SelectedSeat { get; set; }

        public SeatReservationViewModel(long id, long reservationId)
        {
            SeatReservation = _reservationService.GetSeatReservationById(id).Result;
            _isNew = false;
        }

        public SeatReservationViewModel(long reservationId)
        {
            _isNew = true;
            SelectedSeat = new Seat();
            SeatReservation = new SeatReservation
            {
                ReservationId = reservationId
            };
            TicketTypes = _ticketService.GetTicketTypesForReservationId(reservationId).Result.ToList();
        }

        public async void PostToAddNew()
        {
            SeatReservation.TicketId = SelectedTicket.Id;
            SeatReservation.SeatId = SelectedSeat.Id;
            await _reservationService.AddNewSeatReservation(SeatReservation);
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
