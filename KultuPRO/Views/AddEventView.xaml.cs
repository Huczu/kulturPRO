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
using Database.Models;
namespace KulturPRO.Views
{
    /// <summary>
    /// Interaction logic for AddEventView.xaml
    /// </summary>
    public partial class AddEventView : Window
    {
        //new reference of logging system //reflection mówi loggerowi w której klasie jest
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // wybrany,aktualny event
        public static Database.Models.Event ActualEvent = new Database.Models.Event() { Date=DateTime.Now};



        public AddEventView()
        {
            InitializeComponent();

            InitCalendar();

            this.DataContext = ActualEvent;

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

        private void brTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
                imgCover.GetBindingExpression(Image.SourceProperty).UpdateTarget();
            }

            log.Info("Użytkownik Pat zmienił zdjęcie do wydarzenia " + ActualEvent.Name);
            if (ActualEvent != null)
            {
                this.DataContext = ActualEvent;
            }

            

            
        }

        private void btAddEvent_Click(object sender, RoutedEventArgs e)
        {
            bool allTextBoxesNotEmpty = true;

            foreach(var textbox in Utillities.WindowAccessMethods.FindVisualChildren<TextBox>(this))
            {
                if(string.IsNullOrEmpty(textbox.Text))
                {
                    allTextBoxesNotEmpty = false;
                }
            }

            if(dpDate.SelectedDate==null)
            {
                allTextBoxesNotEmpty = false;
            }

            if(string.IsNullOrWhiteSpace(ActualEvent.ImagePath))
            {
                allTextBoxesNotEmpty = false;
            }

            if (allTextBoxesNotEmpty)
            {
                EventService es = new EventService();
                es.AddEvent(ActualEvent);
                MessageBox.Show("Dodano wydarzenie!");
            }
            else
                MessageBox.Show("Nie dodano wydarzenia, brak danych", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
