﻿using System;
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
                Login = "Tester",
                Password = "test",
                FirstName = "Jan",
                Surname = "Tester"
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

            //context.Users.AddRange(new List<User>
            //    {
            //        new User
            //        {
            //            Login = "test1",
            //            Password = "123",
            //            FirstName = "test1",
            //            Surname = "testing1"
            //        },
            //        new User
            //        {
            //            Login = "test2",
            //            Password = "123",
            //            FirstName = "test3",
            //            Surname = "testing4"
            //        },
            //    });
            context.CinemaHalls.AddRange(new List<CinemaHall>
            { 
                new CinemaHall
                {
                    Id = 1,
                    Name = "Sala kinowa 1",
                    MaxRows = 3,
                    MaxColumns = 5
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
                   State = SeatState.Taken,
                   
                   Row=1,
                   Column=2,
                   CinemaHallId=1

                },
                new Seat
                {
                   State = SeatState.Free,
                   
                   Row=1,
                   Column=3,
                   CinemaHallId=1

                },
                new Seat
                {
                   State = SeatState.Free,
                   
                   Row=1,
                   Column=4,
                   CinemaHallId=1
                },
                new Seat
                {
                   State = SeatState.Free,
                   
                   Row=1,
                   Column=5,
                   CinemaHallId=1
                },
                new Seat
                {
                   State = SeatState.NotExists,
                   
                   Row=2,
                   Column=1,
                   CinemaHallId=1
                },
                new Seat
                {
                   State = SeatState.Free,
                   
                   Row=2,
                   Column=2,
                   CinemaHallId=1
                },
                new Seat
                {
                   State = SeatState.Free,
                   
                   Row=2,
                   Column=3,
                   CinemaHallId=1
                },
                new Seat
                {
                   State = SeatState.Free,
                   
                   Row=2,
                   Column=4,
                   CinemaHallId=1
                },
                new Seat
                {
                   State = SeatState.NotExists,
                   
                   Row=2,
                   Column=5,
                   CinemaHallId=1
                },
                new Seat
                {
                   State = SeatState.NotExists,
                   
                   Row=3,
                   Column=1,
                   CinemaHallId=1
                },
                new Seat
                {
                   State = SeatState.NotExists,
                   
                   Row=3,
                   Column=2,
                   CinemaHallId=1
                },
                new Seat
                {
                   State = SeatState.Free,
                   
                   Row=3,
                   Column=3,
                   CinemaHallId=1
                },
                new Seat
                {
                   State = SeatState.NotExists,
                   
                   Row=3,
                   Column=4,
                   CinemaHallId=1
                },
                new Seat
                {
                   State = SeatState.NotExists,
                   
                   Row=3,
                   Column=5,
                   CinemaHallId=1
                }
            });

            var ticket = new Ticket
            {
                TypeName = "Normalny",
                Price = 20.0,
                DiscountPercentage = 0.0,
                IsDefaultEventPrice = true
            };

            context.Tickets.Add(ticket);

            context.SaveChanges(user.FullName);
            context.Events.AddRange(new List<Event>
            {
                new Event
                {
                    Name = "Benny Hill",
                    Date = DateTime.Today.AddDays(1),
                    Time = new TimeSpan(12,15,0),
                    ImagePath = "Images/papa.jpg",
                    Description = "Film komediowy",
                    CinemaHallId = 1,
                    TicketTypes = new List<Ticket>(new [] {ticket})
                },
                new Event
                {
                    Name = "Królik Bugs - The Movie",
                    Date = DateTime.Today.AddDays(2),
                    Time = new TimeSpan(12,45,0),
                    ImagePath = "Images/urban.jpg",
                    Description = "Film animowany dla młodszych",
                    CinemaHallId = 1,
                    TicketTypes = new List<Ticket>(new [] {ticket})
                },
                new Event
                {
                    Name = "Gwiezdne Wojny 7",
                    Date = DateTime.Today.AddDays(3),
                    Time = new TimeSpan(10,0,0),
                    ImagePath = "Images/vader.jpg",
                    Description = "Najnowsza część sagi",
                    CinemaHallId = 1,
                    TicketTypes = new List<Ticket>(new [] {ticket})
                }
            });
            context.SaveChanges(user.FullName);

            context.Reservations.Add(new Reservation
            {
                EventId = 1,
                PersonFullName = "Jan Tester",
                PhoneNumber = "661123987",
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
                        SeatId = 2,
                        TicketId = 1,
                        IsPaid = false
                    }
                }
            });

            context.SaveChanges(user.FullName);
        }
    }
}
