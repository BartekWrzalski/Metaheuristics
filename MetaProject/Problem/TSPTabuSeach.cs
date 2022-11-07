using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class TSPTabuSeach
    {
        private static int generations = int.Parse(ConfigurationManager.AppSettings.Get("Generations"));
        private static int neihgbors_size = int.Parse(ConfigurationManager.AppSettings.Get("Neighbour_size"));
        private static int tabu_size = int.Parse(ConfigurationManager.AppSettings.Get("Tabu_size"));
        private static Random random = new Random();

        public static void TabuSearchTabuBest(Individual ind, out double[][] results, bool useInverse = true)
        {
            results = new double[generations][];
            Individual best_global = ind;
            Individual best_local = ind;
            List<Individual> TabuList = new List<Individual>(tabu_size);
            TabuList.Add(ind);
            int current_gen = 0;

            while (current_gen < generations)
            {
                Individual[] neighbours = getNeighbours(best_local, useInverse);
                best_local = neighbours[0];
                foreach (Individual neighbour in neighbours)
                {
                    if (!TabuList.Contains(neighbour) && neighbour.fitness > best_local.fitness)
                    {
                        best_local = neighbour;
                    }
                }
                
                if (best_local.fitness > best_global.fitness)
                {
                    best_global = best_local;
                }

                if (TabuList.Count == tabu_size)
                {
                    TabuList.RemoveAt(0);
                }
                TabuList.Add(best_local);

                results[current_gen] = new double[4] { best_global.fitness, best_local.fitness, neighbours.Select(i => i.fitness).Average(), neighbours.Select(i => i.fitness).Min() };
                current_gen += 1;
            }

        }

        public static void TabuSearchTabuAll(Individual ind, out double[][] results, bool useInverse = true)
        {
            results = new double[generations][]; 
            Individual best_global = ind;
            Individual best_local = ind;
            List<Individual> TabuList = new List<Individual>(tabu_size + 1);
            TabuList.Add(ind);
            int current_gen = 0;

            while (current_gen < generations)
            {

                Individual[] neighbours = getNeighbours(best_local, useInverse);
                best_local = neighbours[0];
                foreach (Individual neighbour in neighbours)
                {
                    if (!TabuList.Contains(neighbour) && neighbour.fitness > best_local.fitness)
                    {
                        best_local = neighbour;
                    }
                }

                if (best_local.fitness > best_global.fitness)
                {
                    best_global = best_local;
                }

                if (TabuList.Count == tabu_size + 1)
                {
                    TabuList.RemoveRange(0, neihgbors_size);
                }
                TabuList.AddRange(neighbours);

                results[current_gen] = new double[4] { best_global.fitness, best_local.fitness, neighbours.Select(i => i.fitness).Min(), neighbours.Select(i => i.fitness).Average() };
                current_gen += 1;
            }

        }

        public static Individual[] getNeighbours(Individual best_local, bool useInverse = true)
        {
            Individual[] neighbours = new Individual[neihgbors_size];
            if (useInverse)
            {
                for (int i = 0; i < neihgbors_size; i++)
                {
                    neighbours[i] = inverse(best_local);
                }
            }
            else
            {
                for (int i = 0; i < neihgbors_size; i++)
                {
                    neighbours[i] = swap(best_local);
                }
            }

            return neighbours;
        }

        private static Individual swap(Individual best_local)
        {
            Individual ind = new Individual(best_local);
            int n1 = random.Next(0, ind.cities.Length);
            int n2 = random.Next(0, ind.cities.Length);

            int city = ind.cities[n1];
            ind.cities[n1] = ind.cities[n2];
            ind.cities[n2] = city;
            ind.SetFitness();
            return ind;
        }

        private static Individual inverse(Individual best_local)
        {
            Individual ind = new Individual(best_local);
            int n1 = random.Next(0, ind.cities.Length);
            int n2 = random.Next(0, ind.cities.Length);

            int start = Math.Min(n1, n2);
            int end = Math.Max(n1, n2);

            int[] reversed_sub = new ArraySegment<int>(ind.cities, start, end - start).Reverse().ToArray();

            for (int i = start; i < end; i++)
            {
                ind.cities[i] = reversed_sub[i - start];
            }
            ind.SetFitness();
            return ind;
        }
    }
}
