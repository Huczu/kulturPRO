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
            context.CHall.AddRange(new List<CinemaHall>
            { 
                new CinemaHall
                {
                    CinemaHallId = 1,
                    CinemaHallName = "sala1",
                    x = 2,
                    y = 2
                }
            });
            
            context.SaveChanges();
            context.Seats.AddRange(new List<Seat>
            {
                new Seat
                {
                   Status="2",
                   
                   Row=1,
                   Column=1,
                   CinemaHallId=1

                },
                new Seat
                {
                   Status="1",
                   
                   Row=1,
                   Column=2,
                   CinemaHallId=1

                },
                new Seat
                {
                   Status="1",
                   
                   Row=2,
                   Column=1,
                   CinemaHallId=1

                },
                new Seat
                {
                   Status="2",
                   
                   Row=2,
                   Column=2,
                   CinemaHallId=1
                }
            });
            context.SaveChanges();
        }
    }
}
