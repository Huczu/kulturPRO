/*
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
                return await context.CHall.FindAsync(id);
            }
        }

    }
}
*/