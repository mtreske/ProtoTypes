using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace VariousTests
{
    internal class ParallelTests
    {
        [Test]
        public async Task TestParalletExecutionWithTasklist()
        {
            var tasks = Enumerable.Empty<Task>().ToList();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Delay(10000));
            }

            var stopwatch= Stopwatch.StartNew();
            await Task.WhenAll(tasks);  
            var delay = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine($"Elapsed: {delay} sec");
        }
    }
}