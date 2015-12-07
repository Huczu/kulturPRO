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
    [Description("Rezerwacja")]
    public class Reservation
    {
        [Description("Id")]
        public long Id { get; set; }

        [Description("Id Wydarzenia")]
        public long EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }

        [Description("Imię i nazwisko rezerwującego")]
        public string PersonFullName { get; set; }

        [Description("Telefon rezerwującego")]
        [MaxLength(9)]
        public string PhoneNumber { get; set; }

        public ICollection<SeatReservation> SeatReservations { get; set; }

        public bool IsPaid
        {
            get { return SeatReservations.Any(r => !r.IsPaid); }
        }
    }
}
