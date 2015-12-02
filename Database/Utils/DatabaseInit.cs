using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.CodeFirst;
using Database.Models;


namespace Database.Utils
{
    public class DatabaseInit : SqliteCreateDatabaseIfNotExists<DatabaseContext>
    {
        public DatabaseInit(DbModelBuilder modelBuilder)
            : base(modelBuilder)
        {

        }

        protected override void Seed(DatabaseContext context)
        {
            context.Users.AddRange(new List<User>
            {
                new User
                {
                    Login = "test",
                    Password = "123",
                    FirstName = "test",
                    Surname = "testing"
                },
                new User
                {
                    Login = "testoooooo",
                    Password = "123",
                    FirstName = "testoooooo",
                    Surname = "testing"
                }
            });
            context.CinemaHalls.AddRange(new List<CinemaHall>
            { 
                new CinemaHall
                {
                    Id = 1,
                    Name = "sala1",
                    MaxRows = 2,
                    MaxColumns = 2
                }
            });
            
            context.SaveChanges();
            context.Seats.AddRange(new List<Seat>
            {
                new Seat
                {
                   State = SeatState.Taken,

                   Row=1,
                   Column=1,
                   CinemaHallId=1

                },
                new Seat
                {
                   State = SeatState.NotExists,
                   
                   Row=1,
                   Column=2,
                   CinemaHallId=1

                },
                new Seat
                {
                   State = SeatState.Free,
                   
                   Row=2,
                   Column=1,
                   CinemaHallId=1

                },
                new Seat
                {
                   State = SeatState.Taken,
                   
                   Row=2,
                   Column=2,
                   CinemaHallId=1
                }
            });
            context.SaveChanges();
        }
    }
}
