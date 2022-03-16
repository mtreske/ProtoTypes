using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ConsoleProtoTypes
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var names = new List<string> { "1", "2", "3" };

        //    var caps1 = names
        //       .SelectMany(name => LinqItem.GetCaps(name), (name, Cops) => new { name, Cops });

        //    Console.ReadKey();
        //}

        static async Task Main(string[] args)
        {
            try
            {
                await MultipleTasks();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Show my exception:");
                Console.Write(ex.Message);
            }

        }

        private static async Task MultipleTasks()
        {
            Task task = Task.Run(() => throw new ArgumentException("Exception Task 1"));
            Task secondTask = Task.Run(() => throw new NullReferenceException("Exception Task 2"));

            await Task.WhenAll(task, secondTask);
        }
    }
}