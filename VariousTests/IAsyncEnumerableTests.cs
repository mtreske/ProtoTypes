using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace VariousTests
{
    class ToListAsync
    {
        private IEnumerable<Product> products;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            products = PrepareProducts();
        }

        [TestCase(1000000)]
        public void YieldReadVSRead(int count)
        {
            var sw = Stopwatch.StartNew();
            foreach (var product in ReadProducts(count))
            {
                var prod = product.ToString();
            }
            Console.WriteLine($"yield read took {sw.Elapsed.TotalMilliseconds} ms");

            sw.Restart();
            products = PrepareProducts(count);
            foreach (var product in products)
            {
                var prod = product.ToString();
            }
            Console.WriteLine($"read took {sw.Elapsed.TotalMilliseconds} ms");
        }

        [TestCase(1000000)]
        public async Task AsyncRead(int count)
        {
            var sw = Stopwatch.StartNew();
            await foreach (var product in ReadProductsAsync(count))
            {
                var prod = product.ToString();
            }
            Console.WriteLine($"yield read took {sw.Elapsed.TotalMilliseconds} ms");

            sw.Restart();
            products = PrepareProducts(count);
            foreach (var product in products)
            {
                var prod = product.ToString();
            }
            Console.WriteLine($"read took {sw.Elapsed.TotalMilliseconds} ms");
        }

        [TestCase(1000000)]
        public async Task SelectManyAsyncTest(int count)
        {
            var result = ReadProductsAsync(count)
                .GroupBy(x => x.OrderId)
                .SelectManyAwait(async x =>
                {
                    var result =  await Task.WhenAll(x.Select(x => ToStringAsync(x)).ToEnumerable());
                    return x.Select(x => ToStringAsync(x));
                });
                
                
        }

        private IEnumerable<Product> ReadProducts(int count = 10)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Product(i, $"Product-{i}", i % 2 == 0 ? 1 : 0);
            }
        }

        private async IAsyncEnumerable<Product> ReadProductsAsync(int count = 10)
        {
            await Task.Yield();
            for (int i = 0; i < count; i++)
            {
                yield return new Product(i, $"Product-{i}", i % 2 == 0 ? 1 : 0);
            }
        }

        private async Task<string> ToStringAsync(Product product)
        {
            await Task.Yield();
            return product.ToString();
        }

        private IEnumerable<Product> PrepareProducts(int count = 10)
        {
            var products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                
                products.Add(new Product(i, $"Product-{i}", i % 2 == 0 ? 1 : 0));
            }
            return products;
        }
    }

    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int OrderId { get; set; }

        public Product(int id, string name, int orderId)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;
    }
}