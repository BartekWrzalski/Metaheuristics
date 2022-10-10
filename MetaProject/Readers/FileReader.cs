using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MetaProject.Readers
{
    class FileReader
    {
        private int cities_number = 0;
        private int items_number = 0;
        private float min_speed;
        private float max_speed;
        private float[][] cities;
        private int[][] items;
        
        public FileReader(string textFile)
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
                else if (line.StartsWith("MIN"))
                {
                    min_speed = float.Parse(Regex.Split(line, @"\s+")[2].Replace('.', ','));
                }
                else if (line.StartsWith("MAX"))
                {
                    max_speed = float.Parse(Regex.Split(line, @"\s+")[2].Replace('.', ','));
                }
            }
        }

        public void printInfo()
        {
            Console.WriteLine(cities_number);
            Console.WriteLine(items_number);
            Console.WriteLine(min_speed);
            Console.WriteLine(max_speed);
            
            foreach (float[] city in cities)
            {
                Console.WriteLine(city[0] + " " + city[1] + " " + city[2]);
            }
            foreach (int[] item in items)
            {
                Console.WriteLine(item[0] + " " + item[1] + " " + item[2] + " " + item[3]);
            }
        }
    }
}
