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
            ind.weights = new double[data.cities_number];

            for (int i = 0; i < data.cities_number; i++)
            {
                if (!data.cities_wth_items.ContainsKey(i + 1))
                {
                    continue;
                }

                foreach (int item_id in data.cities_wth_items[i + 1])
                {
                    if (data.items[item_id - 1][2] > ind.capacity)
                    {
                        continue;
                    }

                    if (data.items[item_id - 1][2] / data.items[item_id - 1][1] < 0.5)
                    {
                        ind.capacity -= data.items[item_id - 1][2];
                        stolen_items.Add(data.items[item_id - 1][0]);
                    }
                    else if (data.items[item_id - 1][2] / data.items[item_id - 1][1] < 0.75 && data.items[item_id - 1][2] < 0.33 * ind.capacity)
                    {
                        ind.capacity -= data.items[item_id - 1][2];
                        stolen_items.Add(data.items[item_id - 1][0]);
                    }
                }
                ind.weights[i] = data.capacity - ind.capacity;
            }
            ind.items = stolen_items.ToArray();
        }
    }
}
