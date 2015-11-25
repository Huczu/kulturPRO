using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Database;
using System.Collections.ObjectModel;

namespace KulturPro.ViewModels
{
    public class CinemaHallsRead
    {
        ObservableCollection<string[,]> HallMass = new ObservableCollection<string[,]>();
        public void CinemaHallsAdd()
        {
            string[,] hall = new string[1, 1]; 
            HallMass.Add(hall);
        }
        public ObservableCollection<string> GetHallList()
        {

            ObservableCollection<string> cinemaHall = new ObservableCollection<string>();
            int cnt2 = 0;
            using (var context = new DatabaseContext())
            {
                var xs = (from b in context.CHall
                             
                          orderby b.CinemaHallId
                          select b).ToList();
                  

                foreach (var CinemaHallId in xs)
                {
                    
                    cnt2++;
                  CinemaHallsAdd();
                }
            }
            return cinemaHall;
        }
        public ObservableCollection<string[,]> halls2()
        {
            int resolutionX = 0;    //Parametr Hall (X) Długość
            int resolutionY = 0;
                   
            using (var context = new DatabaseContext())
            {
                var xs = (from b in context.CHall
                         
                         orderby b.CinemaHallId
                          select b).ToList();
           
               


                foreach (var CinemaHallId in xs)
                {
                    resolutionX = CinemaHallId.x;
                    resolutionY = CinemaHallId.y;
                    int kinoid = Convert.ToInt32(CinemaHallId.CinemaHallId);
                    int cnt = kinoid-1;
                    HallMass[cnt] = new string[resolutionY, resolutionX];
                    if (HallMass[cnt].GetLength(0) == resolutionY && HallMass[cnt].GetLength(1) == resolutionX)
                    {
                        for (int j = 0; j < resolutionY; j++)            //subtract the seats in the hall
                        {
                            //We subtract the rows
                            for (int k = 0; k < resolutionX; k++)
                            {
                                var status = context.Seats.Single(a => a.CinemaHallId == kinoid && a.Row == k+1 && a.Column == j+1);
                                string value = status.Status;
                                HallMass[cnt][j, k] = value;   // move into an array
                            }
                        }
                    }
                 }
                        
           }
         return HallMass;

          

            
        }



    }
}

