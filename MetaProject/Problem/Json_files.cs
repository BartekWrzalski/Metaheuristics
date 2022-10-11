using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject.Problem
{
    internal class Json_files
    {
        static JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        public static void save_population_to_file(string path, Population pop)
        {
            string json = JsonSerializer.Serialize(pop, options);
            File.WriteAllText(path, json);
        }

        public static void save_individual_to_file(string path, Individual ind)
        {
            string json = JsonSerializer.Serialize(ind, options);
            File.WriteAllText(path, json);
        }
    }
}
