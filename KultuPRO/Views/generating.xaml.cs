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
    public partial class generating : UserControl
    {
        DatabaseContext db = new DatabaseContext();
        KulturPro.ViewModels.CinemaHallsRead CHallObj = new KulturPro.ViewModels.CinemaHallsRead();
        Button[,] bt;           // tablica miejsc
        ObservableCollection<SeatState[,]> h1;        //lista współrzędnych miejsc
        ObservableCollection<SeatState> f1;
        int height = 30;        //parametry miejsc(elementów) w sali(button)
        int width = 30;
        

        public generating()
        {
            InitializeComponent();
            fill_combo();
            cbCinemaHall_SelectionChanged(null, null);
        }

        void fill_combo()
        {
            Database.Services.HallService hallService = new Database.Services.HallService();

            var xd = hallService.GetHallsOrderedById();

            cbCinemaHall.ItemsSource = xd.ToList();
            cbCinemaHall.SelectedValuePath = "Id";
            cbCinemaHall.SelectedIndex = 0;
        }
        
        public void cbCinemaHall_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            grdMain.Children.Clear();
            

            f1 = CHallObj.GetHallList();
            h1 = CHallObj.halls2();

            grdMain.Columns = h1[cbCinemaHall.SelectedIndex].GetLength(1);
            grdMain.Rows = h1[cbCinemaHall.SelectedIndex].GetLength(0);

            bt = new Button[h1[cbCinemaHall.SelectedIndex].GetLength(0), h1[cbCinemaHall.SelectedIndex].GetLength(1)];
            
            int numberToTagCounter = 0;
            for (int i = 0; i < bt.GetLength(0); i++)
            {

                int columnCounter = 0;
                for (int j = 0; j < bt.GetLength(1); j++)
                {
                    if (h1[cbCinemaHall.SelectedIndex][i, j] == SeatState.NotExists)
                    {   //0=wolna przestrzeń
                        //y = i * height;
                        //if (j == 0)
                        //    x = 0;
                        //x += (width + 1);
                        columnCounter++;
                        numberToTagCounter++;
                        bt[i, j] = new Button();
                        bt[i, j].Height = height;
                        bt[i, j].Width = width;
                        bt[i, j].Content = columnCounter.ToString();
                        bt[i, j].HorizontalAlignment = HorizontalAlignment.Stretch;
                        bt[i, j].VerticalAlignment = VerticalAlignment.Stretch;
                        bt[i, j].Tag = Tuple.Create(i, j, cbCinemaHall.SelectedIndex);
                        bt[i, j].Background = Brushes.White;
                        bt[i, j].Visibility = Visibility.Hidden;
                        //y = i * height;
                        //if (j == 0)
                        //    x = 0;
                        //x += (width + 1);
                        bt[i, j].Margin = new Thickness(1);
                        bt[i, j].Click += Button_Click;
                    }
                    if (h1[cbCinemaHall.SelectedIndex][i, j] == SeatState.Free)
                    {   //1=pierwsza grupa cenowa
                        columnCounter++;
                        numberToTagCounter++;
                        bt[i, j] = new Button();
                        bt[i, j].Height = height;
                        bt[i, j].Width = width;
                        bt[i, j].Content = columnCounter.ToString();
                        bt[i, j].HorizontalAlignment = HorizontalAlignment.Stretch;
                        bt[i, j].VerticalAlignment = VerticalAlignment.Stretch;
                        bt[i, j].Tag = Tuple.Create(i, j, cbCinemaHall.SelectedIndex);
                        bt[i, j].Background = Brushes.Green;
                        //y = i * height;
                        //if (j == 0)
                        //    x = 0;
                        //x += (width + 1);
                        //bt[i, j].Margin = new Thickness(x, y, 0, 0);
                        bt[i, j].Margin = new Thickness(1);
                        bt[i, j].Click += Button_Click;
                        
                        
                    }
                    if (h1[cbCinemaHall.SelectedIndex][i, j] == SeatState.Taken)
                    {   //2=premiumqualityplaces
                        columnCounter++;
                        numberToTagCounter++;
                        bt[i, j] = new Button();
                        bt[i, j].Height = height;
                        bt[i, j].Width = width;
                        bt[i, j].Content = columnCounter.ToString();
                        bt[i, j].Tag = Tuple.Create(i, j, cbCinemaHall.SelectedIndex);
                        bt[i, j].Background = Brushes.Coral;
                        //y = i * height;
                        //if (j == 0)
                        //    x = 0;
                        //x += (width + 1);
                        //bt[i, j].Margin = new Thickness(x, y, 0, 0);
                        bt[i, j].Margin = new Thickness(1);
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

                    Grid.SetRow(grdMain.Children[grdMain.Children.Count - 1],i);
                    Grid.SetColumn(grdMain.Children[grdMain.Children.Count - 1], j);
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
                        status.State = SeatState.Free;

                        context.SaveChanges();
                    }
                    (sender as Button).Background = Brushes.Green;

                }

                else
                {
                    using (var context = new DatabaseContext())
                    {
                        var status = context.Seats.Single(z => z.Row == rzad && z.Column == columna && z.CinemaHallId == chid);
                        status.State = SeatState.Taken;

                        context.SaveChanges();
                    }
                    (sender as Button).Background = Brushes.Coral;
                }
            }
            else
            {
                using (var context = new DatabaseContext())
                {
                    var status = context.Seats.Single(z => z.Row == rzad && z.Column == columna && z.CinemaHallId == chid);
                    status.State = SeatState.NotExists;

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
