using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Globalization;
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

            context.Tickets.Add(new Ticket
            {
                TypeName = "Normalny",
                Price = 20.0,
                DiscountPercentage = 0.0,
                IsDefaultEventPrice = true
            });

            context.SaveChanges(user.FullName);
            context.Events.AddRange(new List<Event>
            {
                new Event
                {
                    Name = "Benny Hill",
                    Date = DateTime.ParseExact("20/04/2015", "dd/mm/yyyy", CultureInfo.InvariantCulture),
                    Time = new TimeSpan(12,15,0),
                    ImagePath = "/Images/papa.jpg",
                    Description = "Film komediowy",
                    DefaultTicketId = 1
                },
                new Event
                {
                    Name = "Królik Bugs - The Movie",
                    Date = DateTime.ParseExact("20/04/2015", "dd/mm/yyyy", CultureInfo.InvariantCulture),
                    Time = new TimeSpan(12,45,0),
                    ImagePath = "/Images/urban.jpg",
                    Description = "Film animowany dla młodszych",
                    DefaultTicketId = 1
                },
                new Event
                {
                    Name = "Gwiezdne Wojny 7",
                    Date = DateTime.ParseExact("20/04/2015", "dd/mm/yyyy", CultureInfo.InvariantCulture),
                    Time = new TimeSpan(10,0,0),
                    ImagePath = "/Images/vader.jpg",
                    Description = "Najnowsza część sagi",
                    DefaultTicketId = 1
                }
            });
            context.SaveChanges(user.FullName);

            context.Reservations.Add(new Reservation
            {
                EventId = 1,
                SeatReservations = new List<SeatReservation>
                {
                    new SeatReservation
                    {
                        SeatId = 1,
                        TicketId = 1,
                        IsPaid = false
                    },
                    new SeatReservation
                    {
                        SeatId = 4,
                        TicketId = 1,
                        IsPaid = false
                    }
                }
            });

            context.SaveChanges(user.FullName);
        }
    }
}
