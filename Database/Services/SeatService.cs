using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using System.Data.Entity;

namespace Database.Services
{
    public class SeatService
    {
        public List<Seat> GetSeats()
        {
            using (var context = new DatabaseContext())
            {
                return context.Seats.OrderBy(ch => ch.Id).ToList();
            }
        }
        public Seat GetSeatsByIdColumnAndRow(int id,int column,int row)
        {
            using (var context = new DatabaseContext())
            {
                return (from s in context.Seats where s.CinemaHallId == id && s.Column == column && s.Row == row select s).First();
            }
        }

        public void Add(Seat Seat)
        {
            using (var context = new DatabaseContext())
            {
                context.Seats.Add(Seat);
                context.SaveChanges();
            }
        }
    }
}
