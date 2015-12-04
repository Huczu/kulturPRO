using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class SeatReservation
    {
        public long Id { get; set; }

        public long ReservationId { get; set; }

        public long TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public bool IsPaid { get; set; }
    }
}
