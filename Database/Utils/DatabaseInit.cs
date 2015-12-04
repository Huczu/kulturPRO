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
            var user = new User
            {
                Login = "test",
                Password = "123",
                FirstName = "test",
                Surname = "testing"
            };

            context.Users.Add(user);
            context.SaveChanges(user.FullName);

            ////uncomment if you want to test auditing paging mechanism
            //for (int i = 0; i < 2000; i++)
            //{
            //    context.Users.Add(new User
            //        {
            //            Login = "test" + i.ToString(),
            //            Password = "123",
            //            FirstName = "test" + i.ToString(),
            //            Surname = "testing" + i.ToString()
            //        });
            //}

            context.Users.AddRange(new List<User>
                {
                    new User
                    {
                        Login = "test1",
                        Password = "123",
                        FirstName = "test1",
                        Surname = "testing1"
                    },
                    new User
                    {
                        Login = "test2",
                        Password = "123",
                        FirstName = "test3",
                        Surname = "testing4"
                    },
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
            
            context.SaveChanges(user.FullName);
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

            context.SaveChanges(user.FullName);
        }
    }
}
