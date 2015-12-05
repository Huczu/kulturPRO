using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Database.Services;
using AutoMapper;
using System.ComponentModel;

//mozliwosc konfigurowania logu w kodzie
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace KulturPRO.Views
{
    /// <summary>
    /// Interaction logic for ShowEventsControl.xaml
    /// </summary>
    public partial class ShowEventsControl : UserControl
    {
        //new reference of logging system //reflection mówi loggerowi w której klasie jest
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // wybrany,aktualny event
        public static Database.Models.Event ActualEvent = null;



        public ShowEventsControl()
        {
            InitializeComponent();

            InitCalendar();

            //List<DateTime> dates = new List<DateTime>();
            //foreach (var evvent in EventsForBackgroundClass.Events)
            //{
            //    dates.Add(evvent.Date);
            //}

            //foreach (var date in dates)
            //    cEvents.SelectedDates.Add(date);
            
        }

        /// <summary>
        /// pobiera wydarzenia do kalendarza
        /// </summary>
        private async void InitCalendar()
        {
            EventService service = new EventService();
            EventsForBackgroundClass.Events = await service.GetEvents();
        }

        /// <summary>
        /// wybiera te wydarzenia z listy, których data jest zgodna z tą, którą wybraliśmy w kalendarzu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            lbEventsThisDay.Items.Clear();

            List<Database.Models.Event> selectedEvents = new List<Database.Models.Event>();

            selectedEvents = EventsForBackgroundClass.Events.Where(evvent => evvent.Date == (DateTime)cEvents.SelectedDate).OrderBy(evvent => evvent.TimeSpanTicks).ToList();
            
            foreach (var evvent in selectedEvents)
            {
                lbEventsThisDay.Items.Add(new Views.EventAsListBoxItemView(evvent));
            }
        }


        /// <summary>
        /// wyszukuje eventy po dacie
        /// </summary>
        /// <param name="date"></param>
        public async void FindEventsByDate(DateTime date)
        {
            EventService service = new EventService();
            EventsForBackgroundClass.Events = await service.GetEventsListByDate(date);
        }

        /// <summary>
        /// pobiera do kontrolek dane o wybranym evencie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbEventsThisDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbEventsThisDay.SelectedItem!=null)
            {
                ActualEvent = (Database.Models.Event)((EventAsListBoxItemView)lbEventsThisDay.SelectedItem).DataContext;
                this.DataContext = ActualEvent;
            }
            else
                this.DataContext = null;
        }


        /// <summary>
        /// pozwala zmienić zdjęcie do wydarzenias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btChangePicture_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Pliki JPEG,JPG, PNG, BMP (*.jpeg,*.jpg,*.png,*.bmp)|*.jpeg;*.jpg;*.png;*.bmp | Pliki JPEG (*.jpeg)|*.jpeg|Pliki JPG(*.jpg)|*.jpg|Pliki PNG(*.png)|*.png|Pliki BMP (*.bmp)|*.bmp|Wszystkie pliki (*.*)|*.*";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (ofd.ShowDialog() == true)
            {
                ActualEvent.ImagePath = ofd.FileName;
            }
            log.Info("Użytkownik Pat zmienił zdjęcie do wydarzenia " + ActualEvent.Name);
            if(ActualEvent!=null)
            {
                this.DataContext = ActualEvent;
            }
        }

        private void btModify_Click(object sender, RoutedEventArgs e)
        {
            if(ActualEvent!=null)
            {
                EventService eventService = new EventService();
                eventService.UpdateEvent(ActualEvent);
            }
        }

        private void btAddEvent_Click(object sender, RoutedEventArgs e)
        {
            AddEventView aev = new AddEventView();
            aev.Show();
        }

        private void btDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            if(ActualEvent!=null)
            {
                EventService service = new EventService();
                service.DeleteEvent(ActualEvent);
                MessageBox.Show("Usuwanie wydarzenia zakończone");
                Utillities.WindowAccessMethods.MainContent = new ShowEventsControl();
            }
        }
    }
    public class EventsForBackgroundClass
    {
        static List<Database.Models.Event> events = new List<Database.Models.Event>();
        public static List<Database.Models.Event> Events
        {
            get
            {
                return events;
            }
            set
            {
                events = value;
                OnStaticPropertyChanged("Events");
            }
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;
        public static void OnStaticPropertyChanged(string propertyName)
        {
            var handler = StaticPropertyChanged;
            if (handler != null)
            {
                StaticPropertyChanged(typeof(ShowEventsControl), new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
