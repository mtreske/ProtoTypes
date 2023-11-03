using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VariousTests
{
    
    internal class AsyncStaticTests
    {
        [Test]
        public async Task Test_that_static_property_is_not_threadsave()
        {
            var tasks = new List<Task>();
            tasks.Add(Task.Run(() => TestModel.WriteToConsole("message Task1", 2000, 1)));
            tasks.Add(Task.Run(() => TestModel.WriteToConsole("message Task2", 0, 2)));
            await Task.WhenAll(tasks);
        }

        [Test]
        public async Task Test_that_static_property_is_not_threadsave_2()
        {
            var tasks = new List<Task>();
            tasks.Add(TestModel.WriteToConsoleAsync("message Task1", 2000, 1));
            tasks.Add(TestModel.WriteToConsoleAsync("message Task2", 0, 2));
            await Task.WhenAll(tasks);
        }
    }

    internal static class TestModel
    {
        public static string _message;

        public static void WriteToConsole(string message, int delayMilliSec, int taskId)
        {
            _message = message;
            Console.WriteLine($"{message}, Task={taskId}, Thread={Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(delayMilliSec);
            Console.WriteLine($"{_message}, Task={taskId}, Thread={Thread.CurrentThread.ManagedThreadId}");
        }

        public static async Task WriteToConsoleAsync(string message, int delayMilliSec, int taskId)
        {
            _message = message;
            await Task.Yield();
            Console.WriteLine($"{message}, Task={taskId}, Thread={Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(delayMilliSec);
            Console.WriteLine($"{_message}, Task={taskId}, Thread={Thread.CurrentThread.ManagedThreadId}");
            
        }
    }
}
