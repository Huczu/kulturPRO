using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using System.Data.Entity;

namespace Database.Services
{
    public class TicketService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<ICollection<Ticket>> GetTicketTypesForReservationId(long eventId)
        {
            log.DebugFormat("getting ticket types for event id {0}", eventId);

            using (var context = new DatabaseContext())
            {
                return await context.Reservations.Where(e => e.Id.Equals(eventId)).Select(e => e.Event.TicketTypes).SingleAsync();
            }
        }
    }
}
