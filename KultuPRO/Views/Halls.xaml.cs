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
    /// Interaction logic for Halls.xaml
    /// </summary>
    public partial class Halls : Window
    {
        public Halls()
        {
            InitializeComponent();

        }
        
        private void brTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void btAccept_Click(object sender, RoutedEventArgs e)
        {
            int anInteger = Convert.ToInt32(tbRows.Text);
            anInteger = int.Parse(tbRows.Text);

            int anInteger2 = int.Parse(tbColumns.Text);
            anInteger2 = int.Parse(tbColumns.Text);
            Database.Services.HallService hallService = new Database.Services.HallService();
            Database.Services.SeatService seatService = new Database.Services.SeatService();

            var CinemaHalls = hallService.GetHallsOrderedByName();
            var Seat = seatService.GetSeats();
           
            if (CinemaHalls.Any(c => c.Name.Equals(tbNameofHall.Text)))
            {
                MessageBox.Show("istnieje juz taka nazwa sali");
            }
            else
            { 
                hallService.Add(
                    new Database.Models.CinemaHall
                    {
                        Name = tbNameofHall.Text,
                        MaxRows = anInteger,
                        MaxColumns = anInteger2
                    });

                for (int i = 1; i <= anInteger; i++)
                {
                    for (int j = 1; j <= anInteger2; j++)
                    {

                        seatService.Add(
                            new Database.Models.Seat
                            {
                                State = Database.Models.SeatState.Free,
                                Row = i,
                                Column = j,
                                CinemaHallId = hallService.GetHallIdByName(tbNameofHall.Text)
                            });
                    }
                }
             
                
                MessageBox.Show("Utworzono " +  tbNameofHall.Text);
                }
                
            }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    }

