using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.CodeFirst;
using Database.Models;
using Database.Utils;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("db")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var initializer = new DatabaseInit(modelBuilder);
            System.Data.Entity.Database.SetInitializer(initializer);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<SeatReservation> SeatReservations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
