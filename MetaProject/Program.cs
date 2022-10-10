using MetaProject.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileReader reader = new FileReader("..\\..\\Data\\trivial_0.ttp");
            reader.printInfo();
        }
    }
}
