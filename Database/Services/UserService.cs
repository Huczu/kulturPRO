using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace Database.Services
{
    public class UserService
    {
        public async Task<User> GetUserById(int id)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Users.FindAsync(id);
            }
        }
    }
}
