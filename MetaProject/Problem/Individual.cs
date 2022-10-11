using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class Individual
    {
        public int[] _cities { get; private set; }
        public int[] _items { get; private set; }

        public Individual(int[] cities, int[] items)
        {
            _cities = cities;
            _items = items;
        }

        public void print_cities_id()
        {
            foreach (int city in _cities)
            {
                Console.Write(city + "->");
            }
        }

        public void print_items_id()
        {
            foreach (int item in _items)
            {
                Console.Write(item + " ");
            }
        }
    }
}
