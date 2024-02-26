using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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

        [Test]
        public async Task Concurrent_ReadWrites()
        {
           var tasks = new List<Task> 
            { 
                Task.Run( async () =>
                {
                    var items = GetStringList();
                    await  WriteAsync(items);
                }),
                Task.Run( async () =>
                {
                    var items = GetStringList(3);
                    await  WriteAsync(items);
                })
            };

           await Task.WhenAll(tasks);

        }
        
        private IEnumerable<string> GetStringList(int count = 2)
        {
            for (int i = 0; i < count; i++)
            {
                yield return $"Item:{i}";
            }
        }

        private Task WriteAsync(IEnumerable<string> items)
        {
            Console.WriteLine($"item count: {items.Count()}");
            Task.Delay(1000);
            return Task.CompletedTask;
        }
    }
}