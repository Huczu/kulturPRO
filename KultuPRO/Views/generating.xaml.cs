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
using Database;


namespace KulturPRO.Views
{
    /// <summary>
    /// Interaction logic for generating.xaml
    /// </summary>
    public partial class generating : Window
    {
        DatabaseContext db = new DatabaseContext();
        KulturPro.ViewModels.CinemaHallsRead CHallObj = new KulturPro.ViewModels.CinemaHallsRead();
        Button[,] bt;           // tablica miejsc
        List<string[,]> h1;     //lista współrzędnych miejsc
        int height = 40;        //parametry miejsc(elementów) w sali(button)
        int width = 40;
        public generating()
        {
            InitializeComponent();
            this.Height = 600;
            this.Width = 1200;
            foreach (string item in CHallObj.GetHallList())
            {
                cbCinemaHall.Items.Add(item);   //wypelnianie comboboxa nazwami halli
            }
            cbCinemaHall.SelectedIndex = 0;
            cbCinemaHall_SelectionChanged(null, null);   //zmiana na rysowanie halli


            //РАБОТА С БД------ЭТА ЧАСТЬ КОДА НЕ РАБОТАЕТ ИЛИ ВЫДАЁТ ОШИБКУ!!!----------------------

            //try
            //{
            //    foreach (Movie item in db.Movie)
            //    {
            //        cbMovie.Items.Add(item.MovieName.ToString());
            //    }
            //    foreach (ShTime item in db.STime)
            //    {
            //        cbShowTime.Items.Add(item.ShowName.ToString());
            //    }
            //}
            //catch (Exception e)
            //{
            //    errLbl.Content = e.Message;
            //}

        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
        }

        private void cbCinemaHall_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grdMain.Children.Clear();
            h1 = new List<string[,]>(CHallObj.ReadHallStructure());
            bt = new Button[h1[cbCinemaHall.SelectedIndex].GetLength(0), h1[cbCinemaHall.SelectedIndex].GetLength(1)];
            double x = 0, y = 0;
            int numberToTagCounter = 0;
            for (int i = 0; i < bt.GetLength(0); i++)
            {

                int columnCounter = 0;
                for (int j = 0; j < bt.GetLength(1); j++)
                {
                    if (Convert.ToInt32(h1[cbCinemaHall.SelectedIndex][i, j]) == 0)
                    {   //0=wolna przestrzeń
                        y = i * height;
                        if (j == 0)
                            x = 0;
                        x += (width + 1);
                    }
                    if (Convert.ToInt32(h1[cbCinemaHall.SelectedIndex][i, j]) == 1)
                    {   //1=pierwsza grupa cenowa
                        columnCounter++;
                        numberToTagCounter++;
                        bt[i, j] = new Button();
                        bt[i, j].Height = height;
                        bt[i, j].Width = width;
                        bt[i, j].Content = columnCounter.ToString();
                        bt[i, j].Tag = numberToTagCounter;
                        bt[i, j].Background = Brushes.Green;
                        y = i * height;
                        if (j == 0)
                            x = 0;
                        x += (width + 1);
                        bt[i, j].Margin = new Thickness(x, y, 0, 0);
                        bt[i, j].Click += Button_Click;
                    }
                    if (Convert.ToInt32(h1[cbCinemaHall.SelectedIndex][i, j]) == 2)
                    {   //2=premiumqualityplaces
                        columnCounter++;
                        numberToTagCounter++;
                        bt[i, j] = new Button();
                        bt[i, j].Height = height;
                        bt[i, j].Width = width;
                        bt[i, j].Content = columnCounter.ToString();
                        bt[i, j].Tag = numberToTagCounter;
                        bt[i, j].Background = Brushes.Coral;
                        y = i * height;
                        if (j == 0)
                            x = 0;
                        x += (width + 1);
                        bt[i, j].Margin = new Thickness(x, y, 0, 0);
                        bt[i, j].Click += Button_Click;
                    }
                }
            }
            for (int i = 0; i < bt.GetLength(0); i++)
            {
                for (int j = 0; j < bt.GetLength(1); j++)
                {
                    if (bt[i, j] == null)
                        continue;                   //jeśli jest zerowe pole
                    grdMain.Children.Add(bt[i, j]); //dodajemy miejsce
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
        }

        private void cbMovie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbShowTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
