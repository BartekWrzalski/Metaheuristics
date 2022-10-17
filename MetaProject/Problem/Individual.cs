using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class Individual
    {
        public float capacity { get; set; }
        public int[] cities { get; set; }
        public float[] speeds { get; set; }
        public int[] items { get; set; }
        public float fitness { get; private set; }

        public Individual(int[] cities, float capacity)
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
        }

        public void SetFitness()
        {
            KNPGreedy.GreedySteal(this);
            fitness = Evaluations.RoadLength(this);
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
    }
}
