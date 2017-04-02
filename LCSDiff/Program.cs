using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCSDiff
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = File.ReadAllLines(@"C:\Users\Long\Desktop\Left");
            var target = File.ReadAllLines(@"C:\Users\Long\Desktop\Right");

            var implementation = new LcsImplementation(source, target);
            var sw = Stopwatch.StartNew();
            implementation.InitializeWeights();
            sw.Stop();
            implementation.GetChangeHistory();
            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms");
        }
    }
}
