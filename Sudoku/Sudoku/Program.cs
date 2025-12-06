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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{

    
    internal class Program
    {

        static void GeneraceZakladnihoPaternu(int[,] HraciPole)
        {
            Random random = new Random();
            
            List<int> PaternProRadek = new List<int>();
            //Generace paternu pro kazdy radek 
            for (int i = 0; i < 9; i++)
            {
                int Cislo = random.Next(1,10);

                if (!PaternProRadek.Contains(Cislo))
                {
                    PaternProRadek.Add(Cislo);
                }
                else
                {
                    i--;
                }
            } 


            List<int> CislaPrvnihoSloupce = new List<int>();

            for (int i = 0; i < 9; i++)
            {
                if (CislaPrvnihoSloupce.Contains(PaternProRadek[0]))
                {
                    int n = PaternProRadek.Count;
                    int k = 3;

                    // Obrácení tří částí
                    PaternProRadek.Reverse(0, n - k);
                    PaternProRadek.Reverse(n - k, k);
                    PaternProRadek.Reverse(0, n);

                    i--;
                }
                else
                {
                    CislaPrvnihoSloupce.Add(PaternProRadek[0]);

                    for (int j = 0; j < 9; j++)
                    {
                        HraciPole[i, j] = PaternProRadek[j];
                    }

                    int n = PaternProRadek.Count;
                    int k = 3;

                    // Obrácení tří částí
                    PaternProRadek.Reverse(0, n - k);
                    PaternProRadek.Reverse(n - k, k);
                    PaternProRadek.Reverse(0, n);
                }
                

            }

        }



        static void VypisPole(int[,] HraciPole)
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

        static void Main(string[] args)
        {
            int[,] HraciPole = new int[9, 9];


            GeneraceZakladnihoPaternu(HraciPole);
            VypisPole(HraciPole);
        }
    }
}
