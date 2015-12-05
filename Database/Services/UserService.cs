using System.Threading.Tasks;
using Database.Models;

namespace Database.Services
{


    /// <summary>
    /// Klasa pobierająca użytkowników z bazy danych
    /// </summary>
    public class UserService
    {
        //new reference of logging system //reflection mówi loggerowi w której klasie jest
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// pobiera użytkownika po id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserById(int id)
        {
            using (var context = new DatabaseContext())
            {
                log.Info("Użytkownik Pat pobrał dane użytkownika przez id");
                return await context.Users.FindAsync(id);
            }
        }
    }
}
