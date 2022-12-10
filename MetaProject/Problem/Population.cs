using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        public double GetBestInd()
        {
            return population.Select(i => i.fitness).Max();
        }
        public double GetWorstInd()
        {
            return population.Select(i => i.fitness).Min();
        }

        public double GetAvgInd()
        {
            return population.Select(i => i.fitness).Average();
        }

        public double GetStd()
        {
            double[] fitnesses = population.Select(i => i.fitness).ToArray();
            double avg = GetAvgInd();
            return Math.Sqrt(fitnesses.Average(f => Math.Pow(f - avg, 2)));
        }

        public void print_best()
        {
            Individual ind = population.Where(i => i.fitness == GetBestInd()).Select(i => i).First();

            Console.WriteLine(ind.capacity);
            Console.WriteLine(Evaluations.loot_value(ind));
            Console.WriteLine(Evaluations.RoadLength(ind));
            ind.print_cities_id();
            ind.print_items_id();
        }

        public void print_population()
        {
            foreach (Individual ind in population){
                ind.print_cities_id();
                ind.print_items_id();
            }
        }
        public void orderPopulation()
        {
            Array.Sort(population, (x, y) => y.fitness.CompareTo(x.fitness));
        }
    }
}
