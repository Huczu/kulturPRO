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
using Database.Models;
using System.Collections.ObjectModel;

namespace KulturPRO.Views
{
    public static class Globals
    {
        public static bool prawda = true;
        
    }
    /// <summary>
    /// Interaction logic for generating.xaml
    /// </summary>
    public partial class generating : Window
    {
        DatabaseContext db = new DatabaseContext();
        KulturPro.ViewModels.CinemaHallsRead CHallObj = new KulturPro.ViewModels.CinemaHallsRead();
        Button[,] bt;           // tablica miejsc
        ObservableCollection<string[,]> h1;        //lista współrzędnych miejsc
        ObservableCollection<string> f1;
        int height = 30;        //parametry miejsc(elementów) w sali(button)
        int width = 30;
        

        public generating()
        {
            InitializeComponent();
            fill_combo();
            cbCinemaHall_SelectionChanged(null, null);
            this.Height = 600;
            this.Width = 1200;

        }
       
        void fill_combo()
        {
            DatabaseContext context = new DatabaseContext();

            var xd = (from c in context.CHall
                      select c).ToList();


            cbCinemaHall.ItemsSource = xd.ToList();
            cbCinemaHall.SelectedValuePath = "CinemaHallId";
            cbCinemaHall.SelectedIndex = 0;

        }
        
        public void cbCinemaHall_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            grdMain.Children.Clear();


            f1 = CHallObj.GetHallList();
            h1 = CHallObj.halls2();
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
                        bt[i, j].Tag = Tuple.Create(i, j, cbCinemaHall.SelectedIndex);
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
                        bt[i, j].Tag = Tuple.Create(i, j, cbCinemaHall.SelectedIndex);
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

            string b = (sender as Button).Tag.ToString();
            string c = b.TrimEnd(')');
            string f = c.TrimStart('(');
            int[] ints = ( f.Split(',')).Select(int.Parse).ToArray();
            int columna = ints[0]+1;
            int rzad = ints[1]+1;
            int chid = ints[2]+1;
            if(Globals.prawda)
            {
                if ((sender as Button).Background == Brushes.Coral)
                {
                    using (var context = new DatabaseContext())
                    {
                        var status = context.Seats.Single(z => z.Row == rzad && z.Column == columna && z.CinemaHallId == chid);
                        status.Status = "1";

                        context.SaveChanges();
                    }
                    (sender as Button).Background = Brushes.Green;

                }

                else
                {
                    using (var context = new DatabaseContext())
                    {
                        var status = context.Seats.Single(z => z.Row == rzad && z.Column == columna && z.CinemaHallId == chid);
                        status.Status = "2";

                        context.SaveChanges();
                    }
                    (sender as Button).Background = Brushes.Coral;
                }
            }
            else if(!Globals.prawda)
            {
                using (var context = new DatabaseContext())
                {
                    var status = context.Seats.Single(z => z.Row == rzad && z.Column == columna && z.CinemaHallId == chid);
                    status.Status = "0";

                    context.SaveChanges();
                }
                (sender as Button).Background = Brushes.White;
            }


        }

       

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("włączono usuwanie miejsc");
            Globals.prawda = false;
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Globals.prawda = true;
            MessageBox.Show("wyłączono usuwanie miejsc");
        }

        private void Przycisk_Click(object sender, RoutedEventArgs e)
        {
            cbCinemaHall_SelectionChanged(null, null);
        }
    }
}
