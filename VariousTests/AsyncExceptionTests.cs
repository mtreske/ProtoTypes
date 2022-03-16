using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VariousTests
{
    [TestFixture]
    public class AsyncExceptionTests
    {
        [Test]
        public async Task TestException()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            var tasks = new Dictionary<int, Task<string>>();

            try
            {
                for (int i = 1; i <= 5; i++)
                {
                    //if (i == 5) cts.Cancel();
                    tasks.Add(i, CallDelayAsync(cts.Token, i));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Message: " + ex.Message);
            }

            await Task.Delay(1000);
            try
            {
                //((var results = await Task.WhenAll(tasks.Select(x => x.Value));

                foreach (var task in tasks)
                {
                    Console.WriteLine($"Execute task {task.Key}");
                    await task.Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("async exception caugth: " + ex.Message);
            }
        }

        [Test]
        public async Task TestExceptionSyncCall()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            var tasks = new List<Task<string>>();

            try
            {
                for (int i = 1; i <= 5; i++)
                {
                    if (i == 3) cts.Cancel();
                    var result = await CallDelayAsync(cts.Token, i);

                    Console.WriteLine($"Result {i}: {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Message: " + ex.Message);
            }
        }

        [Test]
        public async Task TestExceptionsParallel()
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

            try
            {
                await Task.WhenAll(task, secondTask);
            }
            catch (AggregateException ex)
            {
                foreach (var inner in ex.InnerExceptions)
                {
                    Console.WriteLine(inner.Message);
                }
            }
        }


        private Task<string> CallDelayAsync(CancellationToken cancellationToken, int i = 0)
        {
            return CallDelayInternalAsync(cancellationToken, i);
        }

        private Task<string> CallDelayInternalAsync(CancellationToken cancellationToken, int i = 0)
        {
            return DelayInternalAsync(cancellationToken, i);
            //try
            //{
            //    return await DelayInternalAsync(cancellationToken, i);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"internal async exception caugth {i}: " + ex.Message);
            //    throw ex;
            //}
        }

        private async Task<string> DelayInternalAsync(CancellationToken cancellationToken, int i = 0)
        {
            try
            {
                await Task.Delay(100);
                if (i == 3) throw new Exception($"Something happened {i}");
                if (i == 4) throw new ArgumentNullException(nameof(i) + $" = {i}");

                if (cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException($"canceled {i}", new Exception($"Something happened {i}"), cancellationToken);
                    //return $"canceled {i}";
                }

                return $"Done {i}";
            }
            finally
            {
                Console.WriteLine($"finally was hit {i}");
            }
        }

        [Test]
        public async Task TestAsyncException()
        {
            try
            {
                await DoSthAsync();
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString());   
            }
        }

        private async Task DoSthAsync()
        {
            throw new NotImplementedException();
            await Task.Delay(1000);
            
        }
    }
}