using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class KNPGreedy
    {
        public static ProblemData data { get; set; }
        public static void GreedySteal(Individual ind)
        {
            List<int> stolen_items = new List<int>();
            float[] speeds = new float[data.cities_number];

            for (int i = 0; i < data.cities_number; i++)
            {
                foreach (int[] item in data.items.Where(t => t[3] == ind.cities[i]))
                {
                    if (item[2] > ind.capacity)
                    {
                        continue;
                    }

                    if (item[2] / item[1] < 0.5)
                    {
                        ind.capacity -= item[2];
                        stolen_items.Add(item[0]);
                    }
                    else if (item[2] / item[1] < 0.75 && item[2] < 0.33 * ind.capacity)
                    {
                        ind.capacity -= item[2];
                        stolen_items.Add(item[0]);
                    }
                }
                speeds[i] = data.min_speed + (float)(ind.capacity / data.capacity) * (data.max_speed - data.min_speed);
            }
            ind.speeds = speeds;
            ind.items = stolen_items.ToArray();
        }
    }
}
