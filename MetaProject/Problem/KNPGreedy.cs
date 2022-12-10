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
            ind.capacity = data.capacity;
            List<int> stolen_items = new List<int>();
            List<double> speeds = new List<double>();

            speeds.Add(data.max_speed);

            foreach(int city in ind.cities.Skip(1))
            {
                if (!data.cities_wth_items.ContainsKey(city))
                {
                    continue;
                }

                foreach (int item_id in data.cities_wth_items[city])
                {
                    if (data.items[item_id - 1][2] > ind.capacity)
                    {
                        continue;
                    }

                    if (data.profits[item_id - 1][0])
                    {
                        ind.capacity -= data.items[item_id - 1][2];
                        stolen_items.Add(data.items[item_id - 1][0]);
                    }
                    else if (data.profits[item_id - 1][1] && data.items[item_id - 1][2] < 0.33 * ind.capacity)
                    {
                        ind.capacity -= data.items[item_id - 1][2];
                        stolen_items.Add(data.items[item_id - 1][0]);
                    }
                }
                speeds.Add(data.max_speed - (data.capacity - ind.capacity) * (data.max_speed - data.min_speed) / data.capacity);
            }
            ind.items = stolen_items.ToArray();
            ind.speeds = speeds.ToArray();
        }
    }
}
