using DeepCopyExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MetaProject.Problem
{
    internal class TspHeuristics
    {
        public static ProblemData data { get; set; }
        private static int population_size = int.Parse(ConfigurationManager.AppSettings.Get("Population"));
        private static int generations = int.Parse(ConfigurationManager.AppSettings.Get("Generations"));
        private static float crossover_rate = float.Parse(ConfigurationManager.AppSettings.Get("Crossover_rate"));
        private static float mutation_rate = float.Parse(ConfigurationManager.AppSettings.Get("Mutation_rate"));
        private static float tour_size_per = float.Parse(ConfigurationManager.AppSettings.Get("Tour_size_per"));
        private static int roulette_power = int.Parse(ConfigurationManager.AppSettings.Get("Roulette_power"));
        private static Random random = new Random();

        public static Population GANextPopulation(Population population)
        {
            for (int i = 1; i < generations; i++)
            {
                Console.WriteLine(population.GetBestInd() + " " + population.GetAvgInd());

                Individual[] selected = select_tournament(population);
                population = ox_crossover(selected);
                inverse_mutation(population);
            }
            return population;
        }

        private static Individual[] select_tournament(Population population)
        {
            int size = (int) (population_size * tour_size_per);
            Individual[] selected = new Individual[population_size];
            Individual best = null;
            for (int i = 0; i < population_size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var ind = population.population[random.Next(0, population_size)];
                    if (best is null || best.fitness > ind.fitness)
                    {
                        best = ind;
                    }
                }
                selected[i] = best;
                best = null;
            }

            return selected;
        }

        private static Individual[] select_roulette(Population population)
        {
            Individual[] selected = new Individual[population_size];

            double[] probs = population.population.Select(c => 1f / Math.Pow(c.fitness, roulette_power)).ToArray();

            probs = Enumerable.Range(0, probs.Length).Select(a => a == 0 ? probs[a] : probs[a] += probs[a - 1]).ToArray();

            for (int i = 0; i < population_size; i++)
            {
                double chosen = random.NextDouble() * probs.Last();
                for (int j = 0; j < population_size; j++)
                {
                    if (chosen < probs[j])
                    {
                        selected[i] = population.population[j];
                        break;
                    }
                }
            }

            return selected;
        }

        private static Population pmx_crossover(Individual[] selected)
        {
            Individual[] new_population = new Individual[population_size];
            for (int i = 0; i < population_size; i+=2)
            {
                if (random.NextDouble() > crossover_rate)
                {
                    // possibly is faster vvv
                    // var copied_parent = selected[random.Next(0, population_size)].DeepCopyByExpressionTree();
                    Individual copied_parent = new Individual(selected[random.Next(0, population_size)]);
                    new_population[i] = copied_parent;
                    copied_parent = new Individual(selected[random.Next(0, population_size)]);
                    new_population[i + 1] = copied_parent;
                    continue;
                }

                int[] parent1 = selected[random.Next(0, population_size)].cities;
                int[] parent2 = selected[random.Next(0, population_size)].cities;

                int n1 = random.Next(0, parent1.Length);
                int n2 = random.Next(0, parent1.Length);

                int start = Math.Min(n1, n2);
                int end = Math.Max(n1, n2);

                int[] child_1_cities = parent1.Clone() as int[];
                int[] child_2_cities = parent2.Clone() as int[];

                for (int j = start; j < end + 1; j++)
                {
                    int c1 = child_1_cities[j];
                    int c2 = child_2_cities[j];

                    child_1_cities[child_1_cities.Select((city, index) => new { city, index }).Where(pair => pair.city == c2).Select(pair => pair.index).First()] = c1;
                    child_2_cities[child_2_cities.Select((city, index) => new { city, index }).Where(pair => pair.city == c1).Select(pair => pair.index).First()] = c2;

                    child_1_cities[j] = c2;
                    child_2_cities[j] = c1;
                }
                new_population[i] = new Individual(child_1_cities, data.capacity);
                new_population[i + 1] = new Individual(child_2_cities, data.capacity);
            }
            return new Population(new_population);
        }

        private static Population ox_crossover(Individual[] selected)
        {
            Individual[] new_population = new Individual[population_size];

            for (int i = 0; i < population_size; i++)
            {
                if (random.NextDouble() > crossover_rate)
                {
                    // possibly is faster vvv
                    // var copied_parent = selected[random.Next(0, population_size)].DeepCopyByExpressionTree();
                    Individual copied_parent = new Individual(selected[random.Next(0, population_size)]);
                    new_population[i] = copied_parent;
                    continue;
                }

                int[] parent1 = selected[random.Next(0, population_size)].cities;
                int[] parent2 = selected[random.Next(0, population_size)].cities;

                int n1 = random.Next(0, parent1.Length);
                int n2 = random.Next(0, parent1.Length);

                int start = Math.Min(n1, n2);
                int end = Math.Max(n1, n2);

                Queue<int> sub_parent1 = new Queue<int>(new ArraySegment<int>(parent1, start, end - start + 1));
                Queue<int> sub_parent2 = new Queue<int>(parent2.Where(x => !sub_parent1.Contains(x)));

                int[] new_cities = new int[parent1.Length];

                for (int j = 0; j < parent1.Length; j++)
                {
/*                    foreach (int a in sub_parent1)
                    {
                        Console.Write(a + " ");
                    }
                    Console.WriteLine();
                    foreach (int a in sub_parent2)
                    {
                        Console.Write(a + " ");
                    }
                    Console.WriteLine();
*/
                    new_cities[j] = j > end || j < start ? sub_parent2.Dequeue() : sub_parent1.Dequeue();
                }
                new_population[i] = new Individual(new_cities, data.capacity);
            }
            return new Population(new_population);
        }

        private static Population swap_mutation(Population population)
        {
            foreach (Individual ind in population.population)
            {
                if (random.NextDouble() > mutation_rate)
                {
                    ind.SetFitness();
                    continue;
                }

                int n1 = random.Next(0, ind.cities.Length);
                int n2 = random.Next(0, ind.cities.Length);

                int city = ind.cities[n1];
                ind.cities[n1] = ind.cities[n2];
                ind.cities[n2] = city;
                ind.SetFitness();
            }
            return population;
        }

        private static Population inverse_mutation(Population population)
        {
            foreach (Individual ind in population.population)
            {
                if (random.NextDouble() > mutation_rate)
                {
                    ind.SetFitness();
                    continue;
                }

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
            }
            return population;
        }
    }
}
