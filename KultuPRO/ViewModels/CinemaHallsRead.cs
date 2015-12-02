using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Database;
using System.Collections.ObjectModel;
using Database.Models;
using System.Windows;

namespace KulturPro.ViewModels
{
    public class CinemaHallsRead
    {
        ObservableCollection<SeatState[,]> HallMass = new ObservableCollection<SeatState[,]>();
        public void CinemaHallsAdd()
        {
            SeatState[,] hall = new SeatState[1, 1]; 
            HallMass.Add(hall);
        }
        public ObservableCollection<SeatState> GetHallList()
        {

            ObservableCollection<SeatState> cinemaHall = new ObservableCollection<SeatState>();

            Database.Services.HallService hallService = new Database.Services.HallService();


            var Halls = hallService.GetHallsOrderedById();

            foreach (var CinemaHall in Halls)
            {
                CinemaHallsAdd();
            }

            return cinemaHall;
        }

        public ObservableCollection<SeatState[,]> halls2()
        {
            int resolutionX = 0;    //Parametr Hall (X) Długość
            int resolutionY = 0;
            Database.Services.SeatService seatService = new Database.Services.SeatService();
            Database.Services.HallService hallService = new Database.Services.HallService();
            var halls = hallService.GetHallsOrderedById();
           
            foreach (var CinemaHall in halls)
            {
                resolutionX = CinemaHall.MaxRows;
                resolutionY = CinemaHall.MaxColumns;
                int kinoid = Convert.ToInt32(CinemaHall.Id);
                int cnt = kinoid-1;
                HallMass[cnt] = new SeatState[resolutionY, resolutionX];
                if (HallMass[cnt].GetLength(0) == resolutionY && HallMass[cnt].GetLength(1) == resolutionX)
                {
                    for (int j = 0; j < resolutionY; j++)            //subtract the seats in the hall
                    {
                        //We subtract the rows
                        for (int k = 0; k < resolutionX; k++)
                        {
                            var status = seatService.GetSeatsByIdColumnAndRow(kinoid, j+1, k+1);
                            SeatState value = status.State;
                            HallMass[cnt][j, k] = value;   // move into an array
                        }
                    }
                }
            }

            return HallMass;        
        }

    }
}

