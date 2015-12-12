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
                    .Include(r => r.Event)
                    .Include(r => r.SeatReservations)
                    .Include(r => r.SeatReservations.Select(sr => sr.Seat))
                    .Include(r => r.SeatReservations.Select(sr => sr.Ticket))
                    .Where(r => r.Id.Equals(id))
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<bool> CanReserveForEvent(long eventId)
        {
            log.DebugFormat("checking if can do reservation for event id = {0}", eventId);

            using (var context = new DatabaseContext())
            {
                var reservedSeats = await context.SeatReservations.Where(sr => sr.Reservation.EventId.Equals(eventId)).CountAsync();
                var seats = await context.Events.Where(e => e.Id.Equals(eventId)).Include(e => e.CinemaHall.Seats).FirstAsync();
                var seatsAvailible = seats.CinemaHall.Seats.Count(s => s.State.Equals(SeatState.Free) || s.State.Equals(SeatState.Taken));

                return reservedSeats < seatsAvailible;
            }
        }

        public async Task<ICollection<SeatReservation>> GetSeatReservationsForReservationId(long reservationId)
        {
            log.DebugFormat("getting seat reservations for reservation {0}", reservationId);

            using (var context = new DatabaseContext())
            {
                return await context.SeatReservations.Where(sr => sr.ReservationId.Equals(reservationId)).Include(sr => sr.Ticket).Include(sr => sr.Seat).ToListAsync();
            }
        }

        public async Task<long> AddReservationForEventId(Reservation reservation)
        {
            log.DebugFormat("adding new reservation for {0} seats for event {1}", reservation.SeatReservations.Count, reservation.EventId);

            using (var context = new DatabaseContext())
            {
                context.Reservations.Add(reservation);
                await context.SaveChangesAsync("USEROW NIE MA A A A");

                log.DebugFormat("created new reservation with id {0}", reservation.Id);

                return reservation.Id;
            }
        }

        public async Task<SeatReservation> GetSeatReservationById(long id)
        {
            log.DebugFormat("getting seat reservation with id {0}", id);

            using (var context = new DatabaseContext())
            {
                return await context.SeatReservations.Include(sr => sr.Seat).Include(sr => sr.Ticket).Where(sr => sr.Id.Equals(id)).SingleAsync();
            }
        }

        public async Task AddNewSeatReservation(SeatReservation seatReservation)
        {
            log.DebugFormat("adding new seat reservation for reservation id {0", seatReservation.ReservationId);

            using (var context = new DatabaseContext())
            {
                context.SeatReservations.Add(seatReservation);
                await context.SaveChangesAsync();
            }

            log.DebugFormat("added new seat reservation with id = {0}", seatReservation.Id);
        }
    }
}
