using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class Evaluations
    {
        public static float speed = 1.0f;
        public static ProblemData data;
        public static float RoadLength(Individual ind)
        {
            float road = 0.0f;
            for (int i = 0; i < ind.cities.Length; i++)
            {
                if (i == ind.cities.Length - 1)
                {
                    road += data.distances[ind.cities.Last() - 1][ind.cities.First() - 1] / ind.speeds[i];
                }
                else
                {
                    road += data.distances[ind.cities[i] - 1][ind.cities[i + 1] - 1] / ind.speeds[i];
                }
            }
            return road;
        }

        public static int loot_value(Individual ind)
        {
            var value = 0;
            foreach (int item_id in ind.items)
            {
                value += data.items[item_id - 1][1];
            }
            return value;
        }
    }
}
