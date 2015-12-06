using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using System.Data.Entity;

namespace Database.Services
{
    public class ReservationService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<ICollection<Reservation>> GetReservationsForEventId(long id)
        {
            using (var context = new DatabaseContext())
            {
                log.DebugFormat("Getting reservations for event id = {0}", id);

                var reservationList = await context.Reservations
                    .Where(r => r.EventId.Equals(id))
                    .Include(r => r.SeatReservations)
                    .ToListAsync();

                log.DebugFormat("Found {0} elements", reservationList.Count);
                return reservationList;
            }
        }

        public async Task<Reservation> GetReservationById(long id)
        {
            using (var context = new DatabaseContext())
            {
                log.DebugFormat("Getting reservation with id = {0}", id);
                
                return await context.Reservations
                    .Include(r => r.SeatReservations)
                    .Include(r => r.Event)
                    .Where(r => r.Id.Equals(id))
                    .FirstOrDefaultAsync();
            }
        }
    }
}
