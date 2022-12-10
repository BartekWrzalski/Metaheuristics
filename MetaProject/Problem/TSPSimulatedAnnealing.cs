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
        private static Random random = new Random();

        public static void SimulatedAnnealingSearch(Individual ind, out double[][] results, int generations, int neihgbors_size, double starting_temp, double scaling_value)
        {
            results = new double[generations][];
            double temperature = starting_temp;
            Individual best_global = ind;
            Individual best_local = ind;
            int current_gen = 0;
            results[current_gen] = new double[3] { best_global.fitness, best_local.fitness, best_local.fitness };
            //Console.WriteLine(string.Join(",", results[current_gen]));
            current_gen++;

            while (current_gen < generations)
            {
                Individual[] neighbours = TSPTabuSeach.getNeighbours(best_local, neihgbors_size, true);

                foreach (Individual neighbour in neighbours)
                {
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

                temperature *= scaling_value;
                results[current_gen] = new double[3] { best_global.fitness, best_local.fitness, neighbours.Select(i => i.fitness).Min() };
                //Console.WriteLine(string.Join(",", results[current_gen]));
                current_gen++;
            }
        }

    }
}
