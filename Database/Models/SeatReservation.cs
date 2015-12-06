using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    [TrackChanges]
    [Description("Rezerwacja miejsca")]
    public class SeatReservation
    {
        [Description("Id")]
        public long Id { get; set; }

        [Description("Id rezerwacji")]
        public long ReservationId { get; set; }

        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }

        [Description("Id biletu")]
        public long TicketId { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }

        [Description("Miejsce")]
        public long SeatId { get; set; }

        [ForeignKey("SeatId")]
        public Seat Seat { get; set; }

        [Description("Czy zapłacono")]
        public bool IsPaid { get; set; }
    }
}
