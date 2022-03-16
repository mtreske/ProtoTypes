using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VariousTests
{
    /// These tests demonstrate how to avoid deadlocks when there is an active SynchronizationContext.
    /// i.e. in an Excel project.
    ///
    /// In the synchronous method use `Task.Run(() => myAsyncMethod()).GetAwaiter().GetResult`.
    ///
    /// Please note, doing "myAsyncMethod().ConfigureAwait(false).GetAwaiter().GetResult"
    /// will still result in deadlocks, so please do not use this pattern - see `ShouldDeadlock1`.
    ///
    /// For more information see:
    ///  See https://docs.microsoft.com/en-us/archive/msdn-magazine/2015/july/async-programming-brownfield-async-development#the-thread-pool-hack
    [TestFixture]
    public class SynchronizationContextTests
    {
        [Test]
        public void ShouldDeadlock1()
        {
            var context = new TestSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(context);

            context.Send(_ =>
           {
               var str = TestAsync();
               Console.WriteLine(str);
           }, null);

            Assert.IsTrue(context.DidDeadlock);
        }

        [Test]
        public void ShouldDeadlock1_2()
        {
            var context = new TestSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(context);

            context.Post(_ =>
            {
                var str = TestAsync().GetAwaiter().GetResult();
                Console.WriteLine(str);
            }, null);

            Assert.IsTrue(context.DidDeadlock);
        }

        [Test]
        public void ShouldDeadlock2()
        {
            var context = new TestSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(context);

            context.Send(_ =>
            {
               context.Send(_=> Test(), null);
                
            }, null);

            Assert.IsTrue(context.DidDeadlock);
        }

        [Test]
        public void ShouldNotDeadlock2()
        {
            var context = new TestSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(context);

            context.Send(_ =>
            {
                Task.Run(() => context.Send(_ => Test(), null));

            }, null);

            Assert.IsFalse(context.DidDeadlock);
        }

        [Test]
        public void ShouldNotDeadlock()
        {
            var context = new TestSynchronizationContext();
            context.Name = "123";
            SynchronizationContext.SetSynchronizationContext(context);

            context.Send(_ =>
           {
               var str = Task.Run(() => TestAsync());
               Console.WriteLine(str);
           }, null);

            Assert.IsFalse(context.DidDeadlock);
        }

        private string Test()
        {
            return "123";
            //return TestAsync().Result;
        }

        private async Task<string> TestAsync()
        {
            await Task.Yield();
            return "hello world";
        }
    }

    public class TestSynchronizationContext : SynchronizationContext
    {
        private SemaphoreSlim semaphore = new SemaphoreSlim(1);
        public bool DidDeadlock { get; set; }
        public string Name { get; set; }

        public override void Send(SendOrPostCallback d, object state)
        {
            if (semaphore.Wait(TimeSpan.FromSeconds(0)))
            {
                d(state);
                semaphore.Release();
            }
            else
            {
                d(state);
                DidDeadlock = true;
            }
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            if (semaphore.Wait(TimeSpan.FromSeconds(0)))
            {
                d(state);
                semaphore.Release();
            }
            else
            {
                d(state);
                DidDeadlock = true;
            }
        }
    }
}