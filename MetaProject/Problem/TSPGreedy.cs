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
            int[] cities = new int[data.cities_number];
            cities[0] = starting_city;

            for (int i = 1; i < data.cities_number; i++)
            {
                float min_dist = data.distances[cities[i - 1] - 1].Where((distance, city) => !cities.Any(c => c == city + 1)).Min();
                int new_city = data.distances[cities[i - 1] - 1].Select((distance, city) => new { city, distance }).Where(pair => pair.distance == min_dist).Select(pair => pair.city + 1).First();
                //Console.WriteLine(cities[i - 1] + " " + min_dist + " " + new_city);
                cities[i] = new_city;
            }
            Individual ind = new Individual(cities, data.capacity);
            ind.SetFitness();
            return ind;
        }
    }
}
