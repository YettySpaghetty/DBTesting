using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace DBTesting.Data
{
    public class DatabaseTesting
    {
        public delegate List<Person> MyFunction(int numOfEntries);

        public void GetExecutionTime(MyFunction f, int numOfEntries)
        {
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();

            f(numOfEntries);

            watch.Stop();

            Console.WriteLine($"Response time: {watch.ElapsedMilliseconds} ms");
        }

    }
}
