using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MetaProject.Problem
{
    class ProblemData
    {
        public int cities_number { get; private set; } = 0;
        public int items_number { get; private set; } = 0;
        public float capacity { get; private set; }
        public float min_speed { get; private set; }
        public float max_speed { get; private set; }
        public float[][] cities { get; private set; }
        public int[][] items { get; private set; }
        public float[][] distances { get; private set; }
        
        public ProblemData(string textFile)
        {
            string[] lines = File.ReadAllLines(textFile);
            int cit = 0;
            int it = 0;

            foreach (string line in lines)
            {
                if (cit != cities_number)
                {
                    string[] str_vals = Regex.Split(line, @"\s+");
                    float[] vals = str_vals.Select(s => float.Parse(s.Replace('.', ','))).ToArray();
                    cities[cit] = vals;
                    cit++;
                }
                else if (it != items_number)
                {
                    string[] str_vals = Regex.Split(line, @"\s+");
                    int[] vals = str_vals.Select(s => int.Parse(s.Replace('.', ','))).ToArray();
                    items[it] = vals;
                    it++;
                }
                else if (line.StartsWith("NODE_"))
                {
                    cit = 0;
                }
                else if (line.StartsWith("ITEMS"))
                {
                    it = 0;
                }
                else if (line.StartsWith("DIMENSION"))
                {
                    cities_number = int.Parse(Regex.Match(line, @"\d+").Value);
                    cit = cities_number;
                    cities = new float[cities_number][];
                }
                else if (line.StartsWith("NUMBER OF"))
                {
                    items_number = int.Parse(Regex.Match(line, @"\d+").Value);
                    it = items_number;
                    items = new int[items_number][];
                }
                else if (line.StartsWith("CAPACITY"))
                {
                    capacity = float.Parse(Regex.Match(line, @"\d+").Value);
                }
                else if (line.StartsWith("MIN"))
                {
                    min_speed = float.Parse(Regex.Split(line, @"\s+")[2].Replace('.', ','));
                }
                else if (line.StartsWith("MAX"))
                {
                    max_speed = float.Parse(Regex.Split(line, @"\s+")[2].Replace('.', ','));
                }
            }

            CalculateDistances();
            setData();
        }

        private void setData()
        {
            Evaluations.data = this;
            RandomMethod.data = this;
            TSPGreedy.data = this;
            KNPGreedy.data = this;
            TSPGenetic.data = this;
        }

        private void CalculateDistances()
        {
            distances = new float[cities_number][];

            for (int i = 0; i < cities_number; i++)
            {
                distances[i] = new float[cities_number];
                for (int j = 0; j < cities_number; j++)
                {
                    if (i == j)
                    {
                        distances[i][j] = float.MaxValue;
                        continue;
                    }
                    distances[i][j] = (float) Math.Sqrt(
                        Math.Pow(cities[i][1] - cities[j][1], 2)
                        + Math.Pow(cities[i][2] - cities[j][2], 2)
                    );
                }
            }
        }

        public void printInfo()
        {
            Console.WriteLine(cities_number);
            Console.WriteLine(items_number);
            Console.WriteLine(capacity);
            Console.WriteLine(min_speed);
            Console.WriteLine(max_speed);
            
/*            foreach (float[] city in cities)
            {
                Console.WriteLine(city[0] + " " + city[1] + " " + city[2]);
            }
            foreach (int[] item in items)
            {
                Console.WriteLine(item[0] + " " + item[1] + " " + item[2] + " " + item[3]);
            }

            foreach (float[] line in distances)
            {
                foreach (float distance in line)
                {
                    Console.Write(String.Format("{0,8:#####.00} ", distance));
                }
                Console.WriteLine();
            }*/
        }
    }
}
