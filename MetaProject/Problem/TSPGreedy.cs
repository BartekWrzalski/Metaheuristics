using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class TSPGreedy
    {
        public static ProblemData data;

        public static Individual getGreedyIndividual(int starting_city)
        {
            List<int> cities_left = new List<int>();
            for (int i = 0; i < data.cities_number; i++)
            {
                cities_left.Add(i);
            }
            cities_left.Remove(starting_city - 1);

            int[] cities = new int[data.cities_number];
            cities[0] = starting_city;

            for (int i = 1; i < data.cities_number; i++)
            {
                double min_dist = double.MaxValue;
                int next_city = 0;
                foreach (int city in cities_left)
                {
                    if (data.distances[cities[i - 1] - 1][city] < min_dist)
                    {
                        min_dist = data.distances[cities[i - 1] - 1][city];
                        next_city = city;
                    }
                }
                cities_left.Remove(next_city);
                cities[i] = next_city + 1;
            }
            Individual ind = new Individual(cities, data.capacity);
            ind.SetFitness();
            //Console.WriteLine(ind.fitness);
            return ind;
        }
    }
}
