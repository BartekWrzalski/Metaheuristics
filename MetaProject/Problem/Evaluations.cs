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
        public static double speed = 1.0;
        public static ProblemData data;
        public static double RoadLength(Individual ind)
        {
            double road = 0.0f;
            for (int i = 0; i < ind.cities.Length - 1; i++)
            {   
                road += data.distances[ind.cities[i] - 1][ind.cities[i + 1] - 1] /
                    (data.min_speed + ind.weights[i] / data.capacity * data.speed_diff);
            }

            road += data.distances[ind.cities.Last() - 1][ind.cities.First() - 1] /
                (data.min_speed + ind.weights.Last() / data.capacity * data.speed_diff);
            
            return road;
        }

        public static double loot_value(Individual ind)
        {
            double value = 0;
            foreach (int item_id in ind.items)
            {
                value += data.items[item_id - 1][1];
            }
            return value;
        }
    }
}
