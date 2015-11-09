using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KulturPro.ViewModels
{
    public class CinemaHallsRead 
    {
        //public ICommand FirstCommand { get; set; }

        List<string[,]> HallMass = new List<string[,]>();   //wykaz pomieszczeń do lokalizacji miejsc
        List<string> path = new List<string>();             //ścieżki plików z wymiarami
        public void CinemaHallsAdd()
        {
            string[,] hall = new string[1, 1]; //Rozmiar początkowy każdego pokoju 1x1, a dopiero potem przychodzi rebindings
            HallMass.Add(hall);
        }

        public List<string> GetHallList()
        {
            string curentDir = Directory.GetCurrentDirectory();
            List<string> cinemaHall = new List<string>();

            foreach (var item in Directory.GetFiles(curentDir, "hall*"))    //Szukamy pliki zaczynające się od "hali"
            {
                cinemaHall.Add(Path.GetFileNameWithoutExtension(item)); //Wartość ta odnosi się do ref
                path.Add(Path.GetFullPath(item));                       //to pozostaje w metodzie
                CinemaHallsAdd();     //dodawaie nowego pokóju w salach tablicy za pomocą konstruktora
            }
            return cinemaHall;
        }

        public List<string[,]> ReadHallStructure()
        {
            int resolutionX = 0;    //Parametr Hall (X) Długość
            int resolutionY = 0;    //Parametr Hall (Y) Szerokość
            int counter = 0;        //mierzenie rozdzielczości

            for (int cnt = 0; cnt < path.Count; cnt++)  //zczytywanie plików z parametrami (wszystkich)
            {
                using (FileStream fs = new FileStream(path[cnt], FileMode.Open, FileAccess.Read))   //FileStream
                {
                    using (StreamReader sr = new StreamReader(fs))                                  //StreamReader
                    {
                        for (int i = 0; i < 2; i++)     //2 przejścia ponieważ 1sze określa wymiar macierzy
                        {                               // 2gie zczytuje wszystkie wartosci
                            //jezeli rozmiar tablicy jest już znany
                            if (HallMass[cnt].GetLength(0) == resolutionY && HallMass[cnt].GetLength(1) == resolutionX)
                            {
                                fs.Seek(0, SeekOrigin.Begin);
                                for (int j = 0; j < HallMass[cnt].GetLength(0); j++)            //odejmowanie miejsc w hali
                                {
                                    string tempString = sr.ReadLine();                  //odejmujemy wiersze
                                    for (int k = 0; k < HallMass[cnt].GetLength(1); k++)
                                    {
                                        HallMass[cnt][j, k] = Convert.ToString(tempString[k]);   //przejście do tablicy
                                    }
                                }
                            }
                            else        //jeżeli wymiary nie zostały znalezione dla hali
                            {
                                fs.Seek(0, SeekOrigin.Begin);
                                do      //definicja parametrów dla hali
                                {
                                    resolutionX = sr.ReadLine().Count();    //długośc
                                    counter++;
                                } while (!sr.EndOfStream);

                                resolutionY = counter;                      //szerokość
                                counter = 0;
                                HallMass[cnt] = new string[resolutionY, resolutionX];  //wymiary wyświetlane
                            }
                        }
                    }
                }
                resolutionX = 0;    //zerowanie wymiarów przed następnym zczytywaniem
                resolutionY = 0;
            }
            return HallMass;
        }
    }
}
