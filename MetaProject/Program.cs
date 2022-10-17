using MetaProject.Problem;
using System;
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
            foreach (string file in files)
            {
                ProblemData problemData = new ProblemData("..\\..\\Data\\" + file + ".ttp");
                
                random(problemData, file);
                greedy(problemData, file);
            }
            */
            string file = "easy_0";
            ProblemData problemData = new ProblemData("..\\..\\Data\\" + file + ".ttp");
            /*
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
            
            // test crossover, rate: 0.7 -> 0.5
            genetic(problemData, file, "crossover_ox_rate_05");
            genetic(problemData, file, "crossover_pmx_rate_05", use_ox_crossover: false);
            
            // test crossover, rate 0.7 -> 0.8
            genetic(problemData, file, "crossover_ox_rate_08");
            genetic(problemData, file, "crossover_pmx_rate_08", use_ox_crossover: false);
            
            //test mutation, swap: 0.01 -> 0.05, inverse: 0.1 -> 0.2
            genetic(problemData, file, "mutation_inverse_02");
            genetic(problemData, file, "mutation_swap_005", use_inverse_mutation: false);
            */
        }

        static void genetic(ProblemData data, string filename, string test,
            bool useTournament = true,
            bool use_ox_crossover = true,
            bool use_inverse_mutation = true
        )
        {
            float[][][] all_results = new float[10][][];
            for (int i = 0; i < 10; i++)
            {
                Population zero = new Population(1000);
                Population final = TSPGenetic.GANextPopulation(zero, out all_results[i], 
                    useTournament, 
                    use_ox_crossover, 
                    use_inverse_mutation
                    );
            }
            float[][] final_result = new float[100][];
            for (int i = 0; i < 100; i++)
            {
                float min_val = float.MaxValue;
                float max_val = float.MinValue;
                float sum_min = 0f;

                foreach (float[][] run_results in all_results)
                {
                    sum_min += run_results[i][0];
                    if (run_results[i][0] < min_val)
                    {
                        min_val = run_results[i][0];
                    }
                    if (run_results[i][1] > max_val)
                    {
                        max_val = run_results[i][1];
                    }
                }
                final_result[i] = new float[3] { min_val, max_val, sum_min / 10 };
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\Results\\genetic_" + filename + "_" + test + ".txt"))
            {
                string best = "";
                foreach (float[] bst in final_result)
                {
                    best = bst[0] + " " + bst[1] + " " + bst[2];
                    file.WriteLine(best);
                }
            }
        }
    
        static void random(ProblemData data, string filename)
        {
            Population random_population = new Population(10000);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\Results\\random_" + filename + ".txt"))
            {
                file.WriteLine(random_population.GetBestInd() + " " + random_population.GetAvgInd() + " " + random_population.GetWorstInd());
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

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\Results\\greedy_" + filename + ".txt"))
            {
                file.WriteLine(greedy_population.GetBestInd() + " " + greedy_population.GetAvgInd() + " " + greedy_population.GetWorstInd());
            }
        }
    }
}
