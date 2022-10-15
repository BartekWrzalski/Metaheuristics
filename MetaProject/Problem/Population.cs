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

        public Population(int size)
        {
            population = new Individual[size];
            
            for (int i = 0; i < size; i++)
            {
                population[i] = RandomMethod.getRandomIndividual();
                population[i].SetFitness();
                // KnpHeuristics.GreedySteal(population[i]);
            }
        }

        public float GetBestInd()
        {
            return population.Select(i => i.fitness).Min();
        }
        public float GetWorstInd()
        {
            return population.Select(i => i.fitness).Max();
        }

        public float GetAvgInd()
        {
            return population.Select(i => i.fitness).Average();
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
