using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class Individual
    {
        public int capacity { get; private set; }
        public int[] cities { get; private set; }
        public int[] items { get; private set; }
        public float fitness;

        public Individual(int[] cities, int capacity)
        {
            this.cities = cities;
            this.items = new int[] { };
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
        }

        public void SetFitness()
        {
            fitness = Evaluations.RoadLength(this);
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
    }
}
