using MetaProject.Problem;
using System;
using System.Diagnostics;

namespace MetaProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProblemData problemData = new ProblemData("..\\..\\Data\\hard_0.ttp");
            /*
            problemData.printInfo();
            Individual ind = RandomMethod.getRandomIndividual();
            ind.print_cities_id();
            float road = Evaluations.RoadLength(ind);
            Console.WriteLine("Road " + road);

            ind = RandomMethod.getRandomIndividual();
            ind.print_cities_id();
            float road2 = Evaluations.RoadLength(ind);
            
            float diff = road2 - road;
            Console.WriteLine(diff);

            Individual ind3 = Greedy.getGreedyIndividual(1);
            ind3.print_cities_id();
            Console.WriteLine(Evaluations.RoadLength(ind3));
            */
            Population zero = new Population(1000);
            Population final = TspHeuristics.GANextPopulation(zero);
        }
    }
}
