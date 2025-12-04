/*

   _____           _       _          
  / ____|         | |     | |         
 | (___  _   _  __| | ___ | | ___   _ 
  \___ \| | | |/ _` |/ _ \| |/ / | | |
  ____) | |_| | (_| | (_) |   <| |_| |
 |_____/ \__,_|\__,_|\___/|_|\_\\__,_|
                                      
                                      
Sudoku - Generace levelu
         Obtiznosti
         Korekce (kontrola spravnosti)
         ----------------------------------
         Pridat GUI (HODNE S REZERVOU skrze WINForms)







Hraci pole:

X
|  1 2 3 |1 2 3 |1 2 3 
|  4 5 6 |4 5 6 |4 5 6 
|  7 8 9 |7 8 9 |7 8 9 
|  ------+------+------
|  1 2 3 |1 2 3 |1 2 3 
|  4 5 6 |4 5 6 |4 5 6 
|  7 8 9 |7 8 9 |7 8 9 
|  ------+------+------
|  1 2 3 |1 2 3 |1 2 3 
|  4 5 6 |4 5 6 |4 5 6 
|  7 8 9 |7 8 9 |7 8 9  

  ----------------------> Y
*/











using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{

    
    internal class Program
    {

        static void PrirazeniHodnotPoli(char[,] HraciPole)
        {

            for (int i = 0; i < HraciPole.GetLength(0); i++)
            {
                for(int j = 0; j < HraciPole.GetLength(0); j++)
                {
                    HraciPole[i, j] = 'X';
                }

            }


        }

        static void VypisPole(char[,] HraciPole)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0 && i != 0)
                    Console.WriteLine("------+-------+------");

                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0 && j != 0)
                        Console.Write("| ");

                    Console.Write(HraciPole[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static void GeneraceProstrednihoSektoru(char[,] HraciPole)
        {
            Random rnd = new Random();
            int CisloVPoradi = 1;
            

            for (int y = 3; y < 6; y++)
            {
                for (int x = 3; x < 6; x++)
                {
                    int RandomX = rnd.Next(3, 6 );
                    int RandomY = rnd.Next(3, 6);


                    if (HraciPole[RandomX, RandomY] == 'X')
                    {
                        HraciPole[RandomX, RandomY] = Convert.ToChar('0' + CisloVPoradi);
                        CisloVPoradi++;
                    }
                    else
                    {
                        x--;
                        continue;
                    }
                    

                }
            }




        }

        static void GeneraceOstatnichCisel(char[,] HraciPole)
        {

            

            for (int y = 0; y < 9; y++)
            {
                List<int> dostupnaCisla = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                // Tento for cyklus se postara o nalezeni vsech jiz predem nastavenych cisel aby se v 1 slopci neopakovali vicekrat
                for (int x = 0; x < 9; x++)
                {
                    if (HraciPole[x,y] != 'X')
                    {
                       dostupnaCisla.Remove(HraciPole[x,y]);
                    }



                }

                // Tento for cyklus uz prirazuje zbyla cisla do jedno slupce
                for(int x  = 0; x < 9; x++)
                {
                    Random rnd = new Random();
                    int NahodneCislo = rnd.Next(1, 10);

                    if (dostupnaCisla.Contains(NahodneCislo))
                    {
                        HraciPole[x, y] = Convert.ToChar('0' + NahodneCislo);
                        dostupnaCisla.Remove(NahodneCislo);

                    }
                    else
                    {
                        x--;
                        continue;

                    }

                }

            } 





        }

        static bool ValidaceHernihoPole(char[,] HraciPole)
        {
            for(int x = 0;x < 9; x++)
            {
                List<int> dostupnaCisla = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                for (int y = 0;y < 9; y++)
                {
                    if (dostupnaCisla.Contains(HraciPole[x, y]))
                    {
                        dostupnaCisla.Remove(HraciPole[x, y]);
                    }
                    else
                    {
                        return false;
                    }


                    
                }

            }
            
           
            return true;

        } 

        static void Main(string[] args)
        {
            char[,] HraciPole = new char[9, 9];
            int pokusy = 0;


            do
            {
                PrirazeniHodnotPoli(HraciPole);
                GeneraceProstrednihoSektoru(HraciPole);
                GeneraceOstatnichCisel(HraciPole);
                VypisPole(HraciPole);
                pokusy++;

            } while(!ValidaceHernihoPole(HraciPole));
            
            Console.Clear();
            VypisPole(HraciPole);
            Console.WriteLine($"\n\n\nHraci plocha byla uspesne vygenerovana po: {pokusy} pokusech");
        }
    }
}
