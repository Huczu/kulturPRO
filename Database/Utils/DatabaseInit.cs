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

            context.SaveChanges(1);
        }
    }
}
