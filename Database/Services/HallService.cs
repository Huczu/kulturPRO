using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace Database.Services
{
    public class HallService
    {
        public async Task<CinemaHall> GetHallById(int id)
        {
            using (var context = new DatabaseContext())
            {
                return await context.CinemaHalls.FindAsync(id);
            }
        }

        public async Task<List<CinemaHall>> GetHallsAsync()
        {
            using (var context = new DatabaseContext())
            {
                return await context.CinemaHalls.OrderBy(ch => ch.Id).ToListAsync();
            }
        }

        public List<CinemaHall> GetHallsOrderedById()
        {
            using (var context = new DatabaseContext())
            {
                return context.CinemaHalls.OrderBy(ch => ch.Id).ToList();
            }
        }

        public List<CinemaHall> GetHallsOrderedByName()
        {
            using (var context = new DatabaseContext())
            {
                return context.CinemaHalls.OrderBy(ch => ch.Name).ToList();
            }
        }

        public void Add(CinemaHall CinemaHall)
        {
            using (var context = new DatabaseContext())
            {
                context.CinemaHalls.Add(CinemaHall);
                context.SaveChanges();
            }
        }

        public long GetHallIdByName(string Name)
        {
            using (var context = new DatabaseContext())
            {
                return (from ch in context.CinemaHalls where ch.Name == Name select ch.Id).First();
            }
        }
    }
}
