using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KulturPRO.ViewModels.Reservations
{
    public class ReservationViewModel
    {
        public long Id { get; set; }

        public ReservationViewModel(long eventId)
        {
            Id = eventId;
        }

        public ReservationViewModel(long eventId, long reservationId)
        {
            Id = eventId;
        }
    }
}
