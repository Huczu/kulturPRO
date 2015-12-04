using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Reservation
    {
        public long Id { get; set; }

        //TODO: Add Event reference once Events branch is merged to master
        public long EventId { get; set; }

        public ICollection<SeatReservation> SeatReservations { get; set; }
    }
}
