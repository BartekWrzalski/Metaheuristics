using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MetaProject.Problem
{
    internal class TSPGenetic
    {
        public static ProblemData data { get; set; }
        private static int population_size;
        private static int generations;
        private static int tour_size;
        private static int roulette_power;
        private static double crossover_rate;
        private static double mutation_rate_inverse;
        private static double mutation_rate_swap;
        private static Random random = new Random();

        public static Population GANextPopulation(Population population, ref List<(double, double)> results, 
            int _population_size,
            int _generations,
            bool useTournament,
            int tournament_size_or_roulette_power,
            bool use_ox_crossover,
            double _crossover_rate,
            bool use_inverse_mutation,
            double _mutation_rate
            )
        {

            population_size = _population_size;
            generations = _generations;

            //results.Add((population.GetBestInd(), population.GetWorstInd()));
            //Console.WriteLine(string.Join(",", results[0]));

            if (useTournament)
            {
                tour_size = tournament_size_or_roulette_power * population_size / 100;
            }
            else
            {
                roulette_power = tournament_size_or_roulette_power;
            }

            crossover_rate = _crossover_rate;

            if (use_inverse_mutation)
            {
                mutation_rate_inverse = _mutation_rate;
            }
            else
            {
                mutation_rate_swap = _mutation_rate;
            }

            for (int i = 0; i < generations; i++)
            {
                Individual[] selected;

                if (useTournament)
                {
                    selected = select_tournament(population);
                }
                else
                {
                    selected = select_roulette(population);
                }

                if (use_ox_crossover)
                {
                    population = ox_crossover(selected);
                }
                else
                {
                    population = pmx_crossover(selected);
                }

                if (use_inverse_mutation)
                {
                    inverse_mutation(population);
                }
                else
                {
                    swap_mutation(population);
                }

                results.Add((population.GetBestInd(), population.GetWorstInd()));
                //Console.WriteLine(string.Join(",", results[i]));
            }
            return population;
        }

        private static Individual[] select_tournament(Population population)
        {
            Individual[] selected = new Individual[population_size];
            for (int i = 0; i < population_size; i++)
            {
                var best = population.population[random.Next(0, population_size)];
                for (int j = 1; j < tour_size; j++)
                {
                    int idx = random.Next(0, population_size);
                    if (best.fitness < population.population[idx].fitness)
                    {
                        best = population.population[idx];
                    }
                }
                selected[i] = best;
            }

            return selected;
        }

        private static Individual[] select_roulette(Population population)
        {
            Individual[] selected = new Individual[population_size];
            double worst = population.GetWorstInd();

            double[] probs = new double[population_size];
            probs[0] = Math.Pow(population.population[0].fitness - worst, roulette_power);
            for (int i = 1; i < population_size; i++)
            {
                probs[i] = Math.Pow(population.population[i].fitness - worst, roulette_power) + probs[i - 1];
            }

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
                    Individual copied_parent = new Individual(selected[random.Next(0, population_size)]);
                    new_population[i] = copied_parent;
                    copied_parent = new Individual(selected[random.Next(0, population_size)]);
                    new_population[i + 1] = copied_parent;
                    continue;
                }

                int[] child_1_cities = selected[random.Next(0, population_size)].cities.Clone() as int[];
                int[] child_2_cities = selected[random.Next(0, population_size)].cities.Clone() as int[];

                int n1 = random.Next(0, child_1_cities.Length - 1);
                int n2 = random.Next(0, child_2_cities.Length);

                int start = Math.Min(n1, n2);
                int end = Math.Max(n1, n2);

                for (int j = start; j < end + 1; j++)
                {
                    int c1 = child_1_cities[j];
                    int c2 = child_2_cities[j];

                    child_1_cities[Array.IndexOf(child_1_cities, c2)] = c1;
                    child_2_cities[Array.IndexOf(child_2_cities, c1)] = c2;

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

                int[] new_cities = new int[parent1.Length];
                List<int> sub_parent2 = new List<int>(parent2);
                
                for (int j = start; j < end + 1; j++)
                {
                    new_cities[j] = parent1[j];
                    sub_parent2.RemoveAt(sub_parent2.IndexOf(parent1[j]));
                }
                
                for (int j = 0; j < start; j++)
                {
                    new_cities[j] = sub_parent2[j];
                }
                for (int j = start; j < sub_parent2.Count; j++)
                {
                    new_cities[j - start + end + 1] = sub_parent2[j];
                }
                new_population[i] = new Individual(new_cities, data.capacity);
            }
            return new Population(new_population);
        }

        private static Population swap_mutation(Population population)
        {
            foreach (Individual ind in population.population)
            {
                if (random.NextDouble() > mutation_rate_swap)
                {
                    if (ind.fitness is double.NaN)
                    {
                        ind.SetFitness();
                    }
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
                if (random.NextDouble() > mutation_rate_inverse)
                {
                    if (ind.fitness is double.NaN)
                    {
                        ind.SetFitness();
                    }
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
