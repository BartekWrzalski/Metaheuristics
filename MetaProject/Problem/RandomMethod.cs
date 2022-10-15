using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class RandomMethod
    {
        public static ProblemData data;
        private static Random rng = new Random();

        public static Individual getRandomIndividual()
        {
            return new Individual(
                cities: data.cities.Select(c => (int) c[0]).OrderBy(c => rng.Next()).ToArray(),
                capacity: data.capacity
            );
        }
    }
}
