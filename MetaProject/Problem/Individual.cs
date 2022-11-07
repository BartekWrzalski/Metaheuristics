using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class Individual : IEquatable<Individual>
    {
        public double capacity { get; set; }
        public double[] weights { get; set; }
        public int[] cities { get; set; }
        public int[] items { get; set; }
        public double fitness { get; private set; } = double.NaN;

        public Individual(int[] cities, double capacity)
        {
            this.cities = cities;
            this.capacity = capacity;
        }

        public Individual(Individual ind)
        {
            capacity = ind.capacity;
            cities = new int[ind.cities.Length];
            for (int i = 0; i < ind.cities.Length; i++)
            {
                cities[i] = ind.cities[i];
            }
            fitness = ind.fitness;
        }

        public void SetFitness()
        {
            KNPGreedy.GreedySteal(this);
            var looted = Evaluations.loot_value(this);
            var road = Evaluations.RoadLength(this);
            //Console.WriteLine(road);
            //print_cities_id();
            // Console.WriteLine(looted + " " + capacity);
            fitness = looted - road;
            //Console.WriteLine(capacity + "  " + Evaluations.loot_value(this));
        }

        public void print_cities_id()
        {
            foreach (int city in cities)
            {
                Console.Write(city + "->");
            }
            Console.WriteLine();
        }

        public void print_items_id()
        {
            foreach (int item in items)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        public bool Equals(Individual other)
        {
            return cities.SequenceEqual(other.cities);
        }
    }
}
