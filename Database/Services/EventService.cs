using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using System.Data.Entity;

//mozliwosc konfigurowania logu w kodzie
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Database.Services
{
    /// <summary>
    /// Klasa pobierająca wydarzenia z bazy danych
    /// </summary>
    public class EventService
    {
        //new reference of logging system //reflection mówi loggerowi w której klasie jest
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// pobiera wydarzenie po id
        /// </summary>
        /// <param name="id">id w bazie danych</param>
        /// <returns></returns>
        public async Task<Event> GetEventById(long id)
        {
            log.Info("Użytkownik Pat pobrał wydarzenie przez id");
            using (var context = new DatabaseContext())
            {
                return await context.Events.FindAsync(id);
            }
        }

        /// <summary>
        /// Pobiera wydarzenie po dacie
        /// </summary>
        /// <param name="date">data wydarzenia</param>
        /// <returns></returns>
        public async Task<Event> GetEventByDate(DateTime date)
        {
            log.Info("Użytkownik Pat pobrał wydarzenie przez datę");
            using (var context = new DatabaseContext())
            {
                return await context.Events.FindAsync(date);
            }
        }

        /// <summary>
        /// Pobiera wydarzenia po dacie
        /// </summary>
        /// <param name="date">data wydarzeń</param>
        /// <returns></returns>
        public async Task<List<Event>> GetEventsListByDate(DateTime date)
        {
            log.Info("Użytkownik Pat pobrał wydarzenia przez datę");
            using (var context = new DatabaseContext())
            {
                return await (from e in context.Events where e.Date == date orderby e.Date select e).ToListAsync();
            }
        }

        /// <summary>
        /// pobiera wszystkie wydarzenia
        /// </summary>
        /// <returns></returns>
        public async Task<List<Event>> GetEvents()
        {
            log.Info("Użytkownik Pat pobrał wydarzenia");
            using (var context = new DatabaseContext())
            {
                return await context.Events.ToListAsync();
            }
        }

        public void UpdateEvent(Event Event)
        {
            log.Info("Użytkownik Pat zmienił wydarzenie " + Event.Name);
            using (var context = new DatabaseContext())
            {
                context.Entry(Event).State = EntityState.Modified;
                context.SaveChangesAsync();
            }
        }

        public void AddEvent(Event Event)
        {
            log.Info("Użytkownik Pat zmienił wydarzenie " + Event.Name);
            using (var context = new DatabaseContext())
            {
                context.Events.Add(Event);
                context.SaveChangesAsync();
            }
        }



        public void DeleteEvent(Event Event)
        {
            log.Info("Użytkownik Pat usunął wydarzenie " + Event.Name);
            using (var context = new DatabaseContext())
            {
                Event evvent = context.Events.Where(e => e.Id == Event.Id).First();
                context.Events.Remove(evvent);
                context.SaveChangesAsync();
            }
        }
    }
}
