using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class Evaluations
    {
        public static float speed = 1.0f;
        public static ProblemData data;
        public static float road_lenght(Individual ind)
        {
            float road = 0f;
            for (int i = 0; i < ind._cities.Length - 1; i++)
            {
                road += speed * (float) Math.Sqrt(
                            Math.Pow(data.cities[ind._cities[i] - 1][1] - data.cities[ind._cities[i + 1] - 1][1], 2)
                            + Math.Pow(data.cities[ind._cities[i] - 1][2] - data.cities[ind._cities[i + 1] - 1][2], 2)
                        );
            }
            road += speed * (float) Math.Sqrt(
                        Math.Pow(data.cities[ind._cities.Last()][1] - data.cities[ind._cities.First()][1], 2)
                        + Math.Pow(data.cities[ind._cities.Last()][2] - data.cities[ind._cities.First()][2], 2)
                    );
            return road;
        }

        public static int loot_value(Individual ind)
        {
            var value = 0;
            foreach (int item_id in ind._items)
            {
                value += data.items[item_id - 1][1];
            }
            return value;
        }
    }
}
