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

            //uncomment if you want to test auditing paging mechanism
            //for (int i = 0; i < 2000; i++ )
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

            context.SaveChanges(user.FullName);
        }
    }
}
