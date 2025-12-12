/*

   _____           _       _          
  / ____|         | |     | |         
 | (___  _   _  __| | ___ | | ___   _ 
  \___ \| | | |/ _` |/ _ \| |/ / | | |
  ____) | |_| | (_| | (_) |   <| |_| |
 |_____/ \__,_|\__,_|\___/|_|\_\\__,_|
                                      
                                      
Sudoku - Generace levelu - DONE
         Obtiznosti
         Korekce (kontrola spravnosti)
         ----------------------------------
         Pridat GUI (HODNE S REZERVOU skrze WPF)

*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
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

            for (int p = 0; p < 9; p++)
            {

                int Radek = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int PocetPrvku = PaternProRadek.Count;
                        int Shift = 3;

                        // Obrácení tří částí
                        PaternProRadek.Reverse(0, PocetPrvku - Shift);
                        PaternProRadek.Reverse(PocetPrvku - Shift, Shift);
                        PaternProRadek.Reverse(0, PocetPrvku);



                        for (int m = 0; m < 9; m++)
                        {
                            HraciPole[Radek, (m + i ) % 9] = PaternProRadek[m];
                        }
                        Radek++;


                    }
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

        static void ZamichejPole(int[,] HraciPole)
        {
            Random random = new Random();

            int PocetMichani = random.Next(20, 50);


            for (int i = 0; i < PocetMichani; i++)
            {

                int VertikalneNeboHorizontalne = random.Next(0, 2);
                int SekceKMichani = random.Next(0, 3);

                if (VertikalneNeboHorizontalne == 1) // Michani bude Vertikalni
                {
                    int[] PoradiPuvodni = new int[3] { 0, 1, 2 };
                    int[] PoradiNove = new int[3];
                    Random r = new Random();

                    for (int m = 0; m < 3; m++)
                    {
                        int Cislo = r.Next(0, 3);
                        if (PoradiNove.Contains(Cislo))
                        {
                            PoradiNove[m] = Cislo;
                        }
                        else
                        {
                            m--;

                        }


                    }


                    int[,] PoleProMichaniSloupcu = new int [3,9];

                    for (int j = 0;j < 3; j++)
                    {
                        for (int k = 0;k < 9; k++)
                        {

                            PoleProMichaniSloupcu[j, SekceKMichani * 3 + j] = HraciPole[PoradiNove[j], SekceKMichani * 3 + j];

                        }
                    }

                    for (int j = 0; j < 3; j++)
                    {
                        for(int k = 0; k < 9; k++)
                        {
                            HraciPole[SekceKMichani * 3 + j, k] = PoleProMichaniSloupcu[j, SekceKMichani * 3 + j];

                        }

                    }

                    //VypisPole(HraciPole);
                    // Thread.Sleep(1000);
                    

                }
                else //Michani bude horizontalni
                {



                }

            }
            




        }

        static void Main(string[] args)
        {
            int[,] HraciPole = new int[9, 9];


            GeneraceZakladnihoPaternu(HraciPole);
            VypisPole(HraciPole);
            Console.WriteLine("\n\n\n\n");
            ZamichejPole(HraciPole);
            VypisPole(HraciPole);
        }
    }
}
