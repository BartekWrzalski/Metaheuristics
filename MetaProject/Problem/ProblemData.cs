using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MetaProject.Problem
{
    class ProblemData
    {
        public double profit_treshold_1 = 0.5;
        public double profit_treshold_2 = 0.25;

        public int cities_number { get; private set; } = 0;
        public int items_number { get; private set; } = 0;
        public double capacity { get; private set; }
        public double min_speed { get; private set; }
        public double max_speed { get; private set; }
        public double speed_diff { get; private set; }
        public double[][] cities { get; private set; }
        public int[][] items { get; private set; }
        public Dictionary<int, List<int>> cities_wth_items { get; private set; }
        public double[][] distances { get; private set; }
        public bool[][] profits { get; private set; }
        
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
                    double[] vals = str_vals.Select(s => double.Parse(s.Replace('.', ','))).ToArray();
                    
                    cities[cit] = vals;
                    cit++;
                }
                else if (it != items_number)
                {
                    string[] str_vals = Regex.Split(line, @"\s+");
                    int[] vals = str_vals.Select(s => int.Parse(s.Replace('.', ','))).ToArray();
                    items[it] = vals;
                    cities_wth_items[vals[3]].Add(vals[0]);
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
                    cities = new double[cities_number][];
                    cities_wth_items = new Dictionary<int, List<int>>();
                    for (int i = 0; i < cities_number; i++)
                    {
                        cities_wth_items.Add(i + 1, new List<int>());
                    }
                }
                else if (line.StartsWith("NUMBER OF"))
                {
                    items_number = int.Parse(Regex.Match(line, @"\d+").Value);
                    it = items_number;
                    items = new int[items_number][];
                }
                else if (line.StartsWith("CAPACITY"))
                {
                    capacity = double.Parse(Regex.Match(line, @"\d+").Value);
                }
                else if (line.StartsWith("MIN"))
                {
                    min_speed = double.Parse(Regex.Split(line, @"\s+")[2].Replace('.', ','));
                }
                else if (line.StartsWith("MAX"))
                {
                    max_speed = double.Parse(Regex.Split(line, @"\s+")[2].Replace('.', ','));
                }
            }
            speed_diff = max_speed - min_speed;
            CalculateDistances();
            CalculateProfits();
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
            distances = new double[cities_number][];

            for (int i = 0; i < cities_number; i++)
            {
                distances[i] = new double[cities_number];
                for (int j = 0; j < cities_number; j++)
                {
                    if (i == j)
                    {
                        distances[i][j] = double.MaxValue;
                        continue;
                    }
                    distances[i][j] = Math.Sqrt(
                        Math.Pow(cities[i][1] - cities[j][1], 2)
                        + Math.Pow(cities[i][2] - cities[j][2], 2)
                    );
                }
            }
        }

        private void CalculateProfits()
        {
            profits = new bool[items_number][];
            if (items[0][1] > items[0][2])
            {
                for (int i = 0; i < items_number; i++)
                {
                    profits[i] = new bool[2];
                    profits[i][0] = (double)items[i][2] / items[i][1] < 1 - profit_treshold_1;
                    profits[i][1] = (double)items[i][2] / items[i][1] < 1 - profit_treshold_2;
                }
            }
            else
            {
                for (int i = 0; i < items_number; i++)
                {
                    profits[i] = new bool[2];
                    profits[i][0] = (double)items[i][1] / items[i][2] > profit_treshold_1;
                    profits[i][1] = (double)items[i][1] / items[i][2] > profit_treshold_2;
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
*/
            foreach (bool[] line in profits)
            {
                foreach (bool distance in line)
                {
                    Console.Write(distance + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
