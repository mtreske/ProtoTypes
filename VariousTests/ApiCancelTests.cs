using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TestWebApi;

namespace VariousTests
{
    [TestFixture]
    class ApiCancelTests
    {
        [Test]
        public async Task CancelApiCall()
        {
            using var client = new WebApplicationFactory<Startup>().CreateClient();
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var sw = Stopwatch.StartNew();
            var resultTask = client.GetAsync(@"api/cancellation", token);
            cts.CancelAfter(2000);
            await Task.WhenAll(new[] { resultTask });
            Console.WriteLine($"Request finished after {sw.Elapsed.TotalSeconds} sec");
        }

        [Test]
        public async Task KillApiCall()
        {
            using var client = new WebApplicationFactory<Startup>().CreateClient();

            var sw = Stopwatch.StartNew();
            var task = new Task(() => client.GetAsync(@"api/cancellation"));
            task.Start();
            await Task.Delay(2000);
            Console.WriteLine($"Request finished after {sw.Elapsed.TotalSeconds} sec");
        }
    }
}