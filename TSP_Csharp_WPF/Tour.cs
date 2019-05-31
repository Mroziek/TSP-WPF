﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Csharp_WPF
{
    public class Tour
    {
        public int[] PathCities;

        public Tour(int numberOfCities)
        {
            PathCities = new int[numberOfCities];
        }

        public void GenerateRandomPath() //at start application needs first random path - it has to be correct, so it's basically random without repeatations
        {
            bool[] isDrawn = new bool[PathCities.Length];
            int randomCity;

            for (int n = 0; n < PathCities.Length;)
            {
                randomCity = GlobalRandom.r.Next(0, PathCities.Length);

                if (isDrawn[randomCity] == false)
                {
                    isDrawn[randomCity] = true;
                    PathCities[n] = randomCity;
                    n++;
                }
            }
        }

        public int CalculateFitness() //calculate path lenght - uses distances matrix (also includes back to hometown)
        {
            int sumDistancePath = 0;

            for (int n = 0; n < PathCities.Length - 1; n++)
            {
                sumDistancePath += Distances.distancesArray[PathCities[n], PathCities[n + 1]];
            }
            sumDistancePath += Distances.distancesArray[PathCities[PathCities.Length - 1], PathCities[0]];

            return sumDistancePath;
        }

        public void PrintPath() //method to print path when needed ex. when new, better path is found - written for console version of algorithm
        {
            foreach (int city in PathCities)
            {
                Console.Write(city + "-");
            }
            Console.Write(CalculateFitness());
        }

        public void MutatePath() //inversion mutation - select 2 random genes - genes between them are inverted 
        {
            int gen1 = GlobalRandom.r.Next(0, PathCities.Length);
            int gen2 = GlobalRandom.r.Next(0, PathCities.Length);

            if (gen1 > gen2) //to be sure that gen1 <= gen2
            {
                int foo = gen2;
                gen2 = gen1;
                gen1 = foo;
            }

            int[] arr1 = new int[gen2 - gen1];

            for (int p = gen1, x = 0; p < gen2; p++, x++)
            {
                arr1[x] = PathCities[p];
            }

            Array.Reverse(arr1); //inversion

            for (int p = gen1, x = 0; p < gen2; p++, x++)
            {
                PathCities[p] = arr1[x];
            }
        }
    }
}
