using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class Population
    {
        public Individual[] population { get; private set; }

        public Population(Individual[] population)
        {
            this.population = population;
        }

        public void print_population()
        {
            foreach (Individual ind in population){
                ind.print_cities_id();
                ind.print_items_id();
            }
        }
    }
}
