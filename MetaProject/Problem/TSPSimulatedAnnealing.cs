using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class TSPSimulatedAnnealing
    {
        private static int generations = int.Parse(ConfigurationManager.AppSettings.Get("Generations"));
        private static double starting_temp = double.Parse(ConfigurationManager.AppSettings.Get("Temperature_start"));
        private static double scaling_temp = double.Parse(ConfigurationManager.AppSettings.Get("Temperature_scaling"));
        private static Random random = new Random();

        public static void SimulatedAnnealingSearch(Individual ind, out double[][] results, bool useInverse = true)
        {
            results = new double[generations][];
            double temperature = starting_temp;
            Individual best_global = ind;
            Individual best_local = ind;
            int current_gen = 0;

            while (current_gen < generations)
            {
                Individual[] neighbours = TSPTabuSeach.getNeighbours(best_local, useInverse);

                foreach (Individual neighbour in neighbours)
                {
                    //Console.WriteLine(neighbour.fitness);
                    if (neighbour.fitness > best_local.fitness)
                    {
                        best_local = neighbour;
                    }
                    else if (random.NextDouble() < Math.Exp((neighbour.fitness - best_local.fitness) / temperature))
                    {
                        best_local = neighbour;
                    }
                }
                if (best_local.fitness > best_global.fitness)
                {
                    best_global = best_local;
                }

                temperature *= scaling_temp;
                results[current_gen] = new double[4] { best_global.fitness, best_local.fitness, neighbours.Select(i => i.fitness).Average(), neighbours.Select(i => i.fitness).Min() };
                current_gen++;
            }
        }

    }
}
