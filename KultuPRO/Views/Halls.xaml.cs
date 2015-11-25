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
        
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            int anInteger = Convert.ToInt32(this.Rows.Text);
            anInteger = int.Parse(this.Rows.Text);

            int anInteger2 = int.Parse(this.Columns.Text);
            anInteger2 = int.Parse(this.Columns.Text);
            using (var context = new DatabaseContext())
            {
                var CinemaHallNames = from c in context.CHall
                                      orderby c.CinemaHallName
                                      select c;
                var Seat = from d in context.Seats
                           select d;

                if (context.CHall.Any(c => c.CinemaHallName.Equals(this.NameofHall.Text)))
                {
                    MessageBox.Show("istnieje juz taka nazwa sali");
                }
                else
                {
                    
                    context.CHall.Add(
                        new Database.Models.CinemaHall
                        {
                            CinemaHallName = this.NameofHall.Text,
                            x = anInteger,
                            y = anInteger2


                        });
                    context.SaveChanges();
                    for (int i = 1; i <= anInteger; i++)
                    {
                        for (int j = 1; j <= anInteger2; j++)
                        {

                            context.Seats.Add(
                                new Database.Models.Seat
                                {
                                    Status = "1",
                                    Row = i,
                                    Column = j,
                                    CinemaHallId = context.CHall.Where(d => d.CinemaHallName.Equals(this.NameofHall.Text)).Select(d => d.CinemaHallId).Single()



                                });
                        }
                    }
                }
             
                
                    MessageBox.Show("Utworzono " +  this.NameofHall.Text);
                        context.SaveChanges();
                    }
                
            }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Halls hall = new Halls();
            hall.Close();
        }
    }
    }

