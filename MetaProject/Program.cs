using MetaProject.Problem;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace MetaProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            // random and greedy for 5 easy files
            string[] files = new string[5] { "easy_0", "easy_1", "easy_2", "easy_3", "easy_4" };
            foreach (string f in files)
            {
                ProblemData data = new ProblemData("..\\..\\Data\\" + f + ".ttp");

                random(data, f);
                greedy(data, f);
            }

            string file = "easy_0";
            ProblemData problemData = new ProblemData("..\\..\\Data\\" + file + ".ttp");
            problemData.printInfo();

            // test selection
            genetic(problemData, file, "selection_tournament");
            genetic(problemData, file, "selection_roulette", useTournament: false);

            // test crossover
            genetic(problemData, file, "crossover_ox");
            genetic(problemData, file, "crossover_pmx", use_ox_crossover: false);

            // test mutation
            genetic(problemData, file, "mutation_inverse");
            genetic(problemData, file, "mutation_swap", use_inverse_mutation: false);

            // test selection, tour: 5 -> 10, roulette_power: 3 -> 5
            genetic(problemData, file, "selection_tournament_10");
            genetic(problemData, file, "selection_roulette_5", useTournament: false);

            // test crossover, rate: 0.7 -> 0.6
            genetic(problemData, file, "crossover_ox_rate_06");
            genetic(problemdata, file, "crossover_pmx_rate_06", use_ox_crossover: false);

            // test crossover, rate 0.7 -> 0.8
            genetic(problemData, file, "crossover_ox_rate_08");
            genetic(problemData, file, "crossover_pmx_rate_08", use_ox_crossover: false);

            // test mutation, swap: 0.01 -> 0.03, inverse: 0.1 -> 0.15
            genetic(problemData, file, "mutation_inverse_015");
            genetic(problemData, file, "mutation_swap_003", use_inverse_mutation: false);

            //test size: 1000 -> 2000, gens 100 -> 500
            genetic(problemData, file, "size_2k_gen_500");

            // run for all easy and 2 hard files
            string[] files = new string[5] { "easy_0", "easy_1", "easy_2", "easy_3", "easy_4" };
            foreach (string file in files)
            {
                ProblemData problemData = new ProblemData("..\\..\\Data\\" + file + ".ttp");

                genetic(problemData, file, "", use_inverse_mutation: false);
            }

            string file = "hard_4";
            ProblemData problemData = new ProblemData("..\\..\\Data\\" + file + ".ttp");
            problemData.printInfo();
            genetic(problemData, file, "5000_100");
            greedy(problemData, file);
            random(problemData, file);

            // all tabu for easy_4

            random(problemData, file);
            greedy(problemData, file);
            tabu(problemData, file, "greedy_All_Inverse");
            tabu(problemData, file, "random_All_Inverse", useGreedy: false);
            tabu(problemData, file, "greedy_One_Inverse", useTabuAll: false);
            tabu(problemData, file, "random_One_Inverse", useGreedy: false, useTabuAll: false);
            tabu(problemData, file, "greedy_All_Swap", useInverse: false);
            tabu(problemData, file, "random_All_Swap", useGreedy: false, useInverse: false);
            tabu(problemData, file, "greedy_One_Swap", useTabuAll: false, useInverse: false);
            tabu(problemData, file, "random_One_Swap", useGreedy: false, useTabuAll: false, useInverse: false);

            // hard_4 tests -> gens_tabu_neighbours
            tabu(problemData, file, "1000_100_10_one", useTabuAll: false);

            tabu(problemData, file, "1000_100_50_one", useTabuAll: false);
            tabu(problemData, file, "1000_200_10_one", useTabuAll: false);
            tabu(problemData, file, "2000_100_10_one", useTabuAll: false);
            tabu(problemData, file, "2000_400_50_one", useTabuAll: false);

            tabu(problemData, file, "greedy", useTabuAll: false, useGreedy: true);
            tabu(problemData, file, "1000_100_10_one", useTabuAll: false);
            tabu(problemData, file, "1000_100_10_one", useTabuAll: false);

            sim_annealing(problemData, file, "profiler");
            sim_annealing(problemData, file, "greedy_20k_50k_9997", useGreedy: true);
            */

            //string file = "medium_0";
            //ProblemData data = new ProblemData("..\\..\\Data\\" + file + ".ttp");
            
            //random(data, file, 10000);
            //greedy(data, file);
            //Console.WriteLine("GENETIC================================================================================================");
            //genetic(data, file, "_base", 2000, 1000, true, 1, true, 0.7, true, 0.15);
            //Console.WriteLine("TABU================================================================================================");
            //tabu(data, file, "_base", 2000, 300, 400);
            //Console.WriteLine("ANNEALING================================================================================================");
            //sim_annealing(data, file, "_base", 15000, 40, 500000, 5);

            //islandEA(data, "hard_0_1", "init",
            //    population_size: 2000,
            //    generations: 1200,
            //    islandAparams: new Dictionary<string, object>() {
            //        { "table_size", 1 },
            //        { "cross_rate", 0.7 },
            //        { "mutation_rate", 0.1 },
            //    },
            //    islandBparams: new Dictionary<string, object>() {
            //        { "table_size", 1 },
            //        { "cross_rate", 0.1 },
            //        { "mutation_rate", 0.65 },
            //    },
            //    skip_gens: 0.1,
            //    frequency: 20,
            //    swapping_rate: 0.2,
            //    elite_change_number: 20,
            //    real_swap: true
            //);
            //islandEA(data, "hard_0_2", "init",
            //    population_size: 2000,
            //    generations: 1200,
            //    islandAparams: new Dictionary<string, object>() {
            //                    { "table_size", 1 },
            //                    { "cross_rate", 0.7 },
            //                    { "mutation_rate", 0.1 },
            //    },
            //    islandBparams: new Dictionary<string, object>() {
            //                    { "table_size", 1 },
            //                    { "cross_rate", 0.1 },
            //                    { "mutation_rate", 0.65 },
            //    },
            //    skip_gens: 0.1,
            //    frequency: 20,
            //    swapping_rate: 0.2,
            //    elite_change_number: 20,
            //    real_swap: false
            //);
            //islandEA(data, "medium_4_1", "init",
            //    population_size: 300,
            //    generations: 500,
            //    islandAparams: new Dictionary<string, object>() {
            //                    { "table_size", 3 },
            //                    { "cross_rate", 0.7 },
            //                    { "mutation_rate", 0.1 },
            //    },
            //    islandBparams: new Dictionary<string, object>() {
            //                    { "table_size", 3 },
            //                    { "cross_rate", 0.1 },
            //                    { "mutation_rate", 0.65 },
            //    },
            //    skip_gens: 0.1,
            //    frequency: 20,
            //    swapping_rate: 0.2,
            //    elite_change_number: 5,
            //    real_swap: true
            //);
            //islandEA(data, "medium_4_2", "init",
            //    population_size: 300,
            //    generations: 500,
            //    islandAparams: new Dictionary<string, object>() {
            //                    { "table_size", 3 },
            //                    { "cross_rate", 0.7 },
            //                    { "mutation_rate", 0.1 },
            //    },
            //    islandBparams: new Dictionary<string, object>() {
            //                    { "table_size", 3 },
            //                    { "cross_rate", 0.1 },
            //                    { "mutation_rate", 0.65 },
            //    },
            //    skip_gens: 0.1,
            //    frequency: 20,
            //    swapping_rate: 0.2,
            //    elite_change_number: 5,
            //    real_swap: false
            //);
        }

        static void islandEA(ProblemData data, string filename, string test,
            int population_size,
            int generations,
            Dictionary<string, object> islandAparams,
            Dictionary<string, object> islandBparams,
            double skip_gens,
            int frequency,
            double swapping_rate,
            int elite_change_number,
            bool real_swap
        )
        {
            Random random = new Random();
            List<(double, double)>[] all_results_A = new List<(double, double)>[10];
            List<(double, double)>[] all_results_B = new List<(double, double)>[10];

            int random_to_swap_number = (int)(population_size * swapping_rate) - elite_change_number;

            for (int i = 0; i < 10; i++)
            {
                Population zeroA = new Population(population_size);
                Population zeroB = new Population(population_size);
                all_results_A[i] = new List<(double, double)>();
                all_results_B[i] = new List<(double, double)>();

                Population currentA = TSPGenetic.GANextPopulation(zeroA, ref all_results_A[i],
                    population_size, (int) (skip_gens * generations),
                    true, (int)islandAparams["table_size"],
                    true, (double)islandAparams["cross_rate"],
                    true, (double)islandAparams["mutation_rate"]
                );
                Population currentB = TSPGenetic.GANextPopulation(zeroB, ref all_results_B[i],
                    population_size, (int) (skip_gens * generations),
                    true, (int)islandBparams["table_size"],
                    true, (double)islandBparams["cross_rate"],
                    false, (double)islandBparams["mutation_rate"]
                );

                int current = (int) (skip_gens * generations);
                while (current < generations)
                {
                    Population nextA = TSPGenetic.GANextPopulation(currentA, ref all_results_A[i], 
                        population_size, frequency, 
                        true, (int) islandAparams["table_size"], 
                        true, (double) islandAparams["cross_rate"],
                        true, (double) islandAparams["mutation_rate"]
                    );
                    Population nextB = TSPGenetic.GANextPopulation(currentB, ref all_results_B[i],
                        population_size, frequency,
                        true, (int) islandBparams["table_size"],
                        true, (double) islandBparams["cross_rate"],
                        false, (double) islandBparams["mutation_rate"]
                    );

                    nextA.orderPopulation();
                    nextB.orderPopulation();

                    if (real_swap)
                    {
                        for (int j = 0; j < elite_change_number; j++)
                        {
                            //Console.WriteLine(String.Format("A{0} <-> B{1}", j, j));
                            var ind_t = nextA.population[j];
                            nextA.population[j] = nextB.population[j];
                            nextB.population[j] = ind_t;
                        }
                        int max_random_index = population_size - elite_change_number - 1;
                        for (int j = 0; j < random_to_swap_number; j++)
                        {
                            int k = random.Next(max_random_index) + elite_change_number;
                            //Console.WriteLine(String.Format("A{0} <-> B{1}", k, k));

                            var ind_t = nextA.population[k];
                            nextA.population[k] = nextB.population[k];
                            nextB.population[k] = ind_t;
                        }
                    }
                    else
                    {
                        int max_random_index = population_size - elite_change_number;
                        for (int j = 0; j < elite_change_number; j++)
                        {
                            int k_A = random.Next(max_random_index) + elite_change_number;
                            int k_B = random.Next(max_random_index) + elite_change_number;
                            //Console.WriteLine(String.Format("A{0} -> B{1}", j, k_B));
                            //Console.WriteLine(String.Format("B{0} -> A{1}", j, k_A));

                            var ind_t = new Individual(nextA.population[j]);
                            nextB.population[k_B] = ind_t;

                            ind_t = new Individual(nextB.population[j]);
                            nextA.population[k_A] = ind_t;
                        }
                        for (int j = 0; j < random_to_swap_number; j++)
                        {
                            int k_A = random.Next(population_size);
                            int k_B = random.Next(population_size);
                            //Console.WriteLine(String.Format("A{0} -> B{1}", k_A, k_B));

                            var ind_t = new Individual(nextA.population[k_A]);
                            nextB.population[k_B] = ind_t;

                            k_A = random.Next(population_size);
                            k_B = random.Next(population_size);
                            //Console.WriteLine(String.Format("B{0} -> A{1}", k_B, k_A));
                            
                            ind_t = new Individual(nextB.population[k_B]);
                            nextA.population[k_A] = ind_t;
                        }
                    }

                    currentA = nextA;
                    currentB = nextB;
                    current += frequency;
                }
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\HybridResult\\hb_" + filename + "_" + test + ".txt"))
            {
                string line = "";
                for (int i = 0; i < generations; i++)
                {
                    var bestA = all_results_A.Select(list => list[i].Item1).Max();
                    var avgA = all_results_A.Select(list => list[i].Item1).Average();
                    var worstA = all_results_A.Select(list => list[i].Item2).Min();

                    var bestB = all_results_B.Select(list => list[i].Item1).Max();
                    var avgB = all_results_B.Select(list => list[i].Item1).Average();
                    var worstB = all_results_B.Select(list => list[i].Item2).Min();

                    line = string.Format("{0} {1} {2} {3} {4} {5}", bestA, avgA, worstA, bestB, avgB, worstB);
                    file.WriteLine(line);
                }
                var bests = all_results_A.Select(list => list.Last().Item1);
                var best_avg = bests.Average();
                var std = Math.Sqrt(bests.Average(v => Math.Pow(v - best_avg, 2)));
                file.WriteLine(std);
                file.WriteLine(string.Format("{0} {1}", bests.Max(), bests.Min()));
            }
        }

        static void genetic(ProblemData data, string filename, string test,
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
            List<(double, double)>[] all_results = new List<(double, double)>[10];
            for (int i = 0; i < 10; i++)
            {
                all_results[i] = new List<(double, double)>();

                Console.WriteLine(i);
                Population zero = new Population(_population_size);
                Population final = TSPGenetic.GANextPopulation(zero, ref all_results[i],
                    _population_size,
                    _generations,
                    useTournament,
                    tournament_size_or_roulette_power,
                    use_ox_crossover,
                    _crossover_rate,
                    use_inverse_mutation,
                    _mutation_rate
                );
                //final.print_best();
            }
           
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\4Results\\genetic_" + filename + "_" + test + ".txt"))
            {
                string line = "";
                for (int i = 0; i < _generations; i++)
                {
                    var bestA = all_results.Select(list => list[i].Item1).Max();
                    var avgA = all_results.Select(list => list[i].Item1).Average();
                    var worstA = all_results.Select(list => list[i].Item2).Min();

                    line = string.Format("{0} {1} {2}", bestA, avgA, worstA);
                    file.WriteLine(line);
                }
                var bests = all_results.Select(list => list.Last().Item1);
                var best_avg = bests.Average();
                var std = Math.Sqrt(bests.Average(v => Math.Pow(v - best_avg, 2)));
                file.WriteLine(std);
                file.WriteLine(string.Format("{0} {1}", bests.Max(), bests.Min()));
            }
        }

        static void tabu(ProblemData data, string filename, string test, int generations, int neihgbors_size, int tabu_size, bool useGreedy = false)
        {
            double[][][] all_results = new double[10][][];
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                Individual start;
                if (useGreedy)
                {
                    start = TSPGreedy.getGreedyIndividual(1);
                }
                else
                {
                    start = RandomMethod.getRandomIndividual();
                    start.SetFitness();
                }
                
                TSPTabuSeach.TabuSearchTabuBest(start, out all_results[i], generations, neihgbors_size, tabu_size);
            }

            double[][] final_result = new double[generations][];
            for (int i = 0; i < generations; i++)
            {
                double best_val_global = double.MinValue;
                double best_val_local = double.MinValue;
                double worst_val = double.MaxValue;
                double sum_best = 0f;

                foreach (double[][] run_results in all_results)
                {
                    sum_best += run_results[i][1];
                    if (run_results[i][0] > best_val_global)
                    {
                        best_val_global = run_results[i][0];
                    }
                    if (run_results[i][1] > best_val_local)
                    {
                        best_val_local = run_results[i][1];
                    }
                    if (run_results[i][2] < worst_val)
                    {
                        worst_val = run_results[i][2];
                    }
                }
                final_result[i] = new double[4] { best_val_global, best_val_local, worst_val, sum_best / 10 };
            }

            double[] last_bests = all_results.Select(l => l.Last()[1]).ToArray();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\4Results\\tabu_" + filename + "_" + test + ".txt"))
            {
                string best = "";
                foreach (double[] bst in final_result)
                {
                    best = bst[0] + " " + bst[1] + " " + bst[2] + " " + bst[3];
                    file.WriteLine(best);
                }
                file.WriteLine(Math.Sqrt(last_bests.Average(v => Math.Pow(v - final_result.Last()[3], 2))));
                file.WriteLine(last_bests.Max() + " " + last_bests.Average() + " " + last_bests.Min());
            }
        }
    
        static void sim_annealing(ProblemData data, string filename, string test, int generations, int neihgbors_size, double starting_temp, double final_temp)
        {
            double[][][] all_results = new double[10][][];
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                Individual start;

                start = RandomMethod.getRandomIndividual();
                start.SetFitness();
                double scaling_value = Math.Pow(final_temp / starting_temp, 1.0 / generations);
                
                TSPSimulatedAnnealing.SimulatedAnnealingSearch(start, out all_results[i], generations, neihgbors_size, starting_temp, scaling_value);
            }

            double[][] final_result = new double[generations][];
            for (int i = 0; i < generations; i++)
            {
                double best_val_global = double.MinValue;
                double best_val_local = double.MinValue;
                double worst_val = double.MaxValue;
                double sum_best = 0f;

                foreach (double[][] run_results in all_results)
                {
                    sum_best += run_results[i][1];
                    if (run_results[i][0] > best_val_global)
                    {
                        best_val_global = run_results[i][0];
                    }
                    if (run_results[i][1] > best_val_local)
                    {
                        best_val_local = run_results[i][1];
                    }
                    if (run_results[i][2] < worst_val)
                    {
                        worst_val = run_results[i][2];
                    }
                }
                final_result[i] = new double[4] { best_val_global, best_val_local, worst_val, sum_best / 10 };
            }

            double[] last_bests = all_results.Select(l => l.Last()[1]).ToArray();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\4Results\\simann_" + filename + "_" + test + ".txt"))
            {
                string best = "";
                foreach (double[] bst in final_result)
                {
                    best = bst[0] + " " + bst[1] + " " + bst[2] + " " + bst[3];
                    file.WriteLine(best);
                }
                file.WriteLine(Math.Sqrt(last_bests.Average(v => Math.Pow(v - final_result.Last()[3], 2))));
                file.WriteLine(last_bests.Max() + " " + last_bests.Average() + " " + last_bests.Min());
            }
        }

        static void random(ProblemData data, string filename, int population_size)
        {
            Population random_population = new Population(population_size);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\4Results\\random_" + filename + ".txt"))
            {
                file.WriteLine(random_population.GetBestInd() + " " + random_population.GetAvgInd() + " " + random_population.GetWorstInd() + " " + random_population.GetStd());
            }
        }

        static void greedy(ProblemData data, string filename)
        {
            Individual[] inds = new Individual[data.cities_number];

            for (int i = 0; i < data.cities_number; i++)
            {
                inds[i] = TSPGreedy.getGreedyIndividual(i + 1);
            }
            Population greedy_population = new Population(inds);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\4Results\\greedy_" + filename + ".txt"))
            {
                file.WriteLine(greedy_population.GetBestInd() + " " + greedy_population.GetAvgInd() + " " + greedy_population.GetWorstInd() + " " + greedy_population.GetStd());
            }
        }
    }
}
