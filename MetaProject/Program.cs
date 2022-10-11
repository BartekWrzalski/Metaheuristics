using MetaProject.Problem;
using System;
using System.Configuration;

namespace MetaProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProblemData reader = new ProblemData("..\\..\\Data\\trivial_0.ttp");
            Evaluations.data = reader;
            RandomMethod._data = reader;
            reader.printInfo();
            //Console.WriteLine(ConfigurationManager.AppSettings.Get("Population"));

            Individual one = RandomMethod.getRandomIndividual();
            //Console.WriteLine(Evaluations.road_lenght(one));
            Population p1 = new Population(new Individual[] {one});

            Json_files.save_population_to_file("..\\..\\Saved results\\pop.json", p1);

            Json_files.save_individual_to_file("..\\..\\Saved results\\ind.json", p1.population[0]);
        }
    }
}
