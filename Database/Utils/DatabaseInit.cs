using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.CodeFirst;
using Database.Models;
using System.Threading;

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

            context.Events.AddRange(new List<Event>
            {
                new Event
                {
                    Name = "Benny Hill",
                    Date = DateTime.Parse("20.04.2015"),
                    Time = new TimeSpan(12,15,0),
                    ImagePath = "/Images/papa.jpg",
                    Description = "Film komediowy"
                },
                new Event
                {
                    Name = "Królik Bugs - The Movie",
                    Date = DateTime.Parse("20.04.2015"),
                    Time = new TimeSpan(12,45,0),
                    ImagePath = "/Images/urban.jpg",
                    Description = "Film animowany dla młodszych"
                },
                new Event
                {
                    Name = "Gwiezdne Wojny 7",
                    Date = DateTime.Parse("20.04.2015"),
                    Time = new TimeSpan(10,0,0),
                    ImagePath = "/Images/vader.jpg",
                    Description = "Najnowsza część sagi"
                }
            });

            context.SaveChanges();       
        }
    }
}
