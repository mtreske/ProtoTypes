using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace VariousTests
{
    [TestFixture]
    public class IEnumerableTests
    {

        [Test]
        public void Late_Materialization()
        {
            var stringList1 = GetStringList();
            var stringList2 = new List<string>();
            stringList2.AddRange(stringList1);
            
            var stringList3 = new List<string>();
            foreach (var item in stringList1)
            {
                stringList3.Add(item);
            }

            Console.WriteLine(string.Join(',', stringList1));
            Console.WriteLine(string.Join(',', stringList2));
            Console.WriteLine(string.Join(',', stringList3));
        }

        [Test]
        public void MemoryAllocation()
        {
            var stringList1 = GetStringList();
            var stringList2 = ReturnArgument(stringList1.ToList());
            var stringList3 = ReturnResult(stringList1);
            
            Assert.That(stringList2.Equals(stringList1), Is.False);
            Assert.That(stringList3.Equals(stringList1), Is.False);
        }

        [Test]
        public void Where_vs_FirstOrDefault()
        {
            var count = 1000;
            var items = GetItemList(count);
            
            Stopwatch sw = Stopwatch.StartNew();
            var item1 = items.Where(x => x.Id == 100 && x.Name1 == "100").FirstOrDefault();
            var firstDuration = sw.ElapsedTicks;
            
            sw.Restart();
            var item2 = items.FirstOrDefault(x => x.Id == 100 && x.Name1 == "100");
            var secondDuration = sw.ElapsedTicks;
            
            Console.WriteLine($"duration for {count} items ->  Where()         : {firstDuration}");
            Console.WriteLine($"duration for {count} items ->  FirstOrDefault(): {secondDuration}");
        }

        private IEnumerable<string> GetStringList(int count = 2)
        {
            for (int i = 0; i < count; i++)
            {
                yield return $"Item:{i}";
            }
        }
        
        private IEnumerable<TestItem> GetItemList(long count = 2)
        {
            for (long i = 0; i < count; i++)
            {
                yield return new TestItem { Id = i, Name1 = i.ToString()};
            }
        }

        private class TestItem
        {
            public long Id { get; set; }
            public string Name1 { get; set; }
            public string Name2 { get; set; }
            public string Name3 { get; set; }
            public string Name4 { get; set; }
            public string Name5 { get; set; }
            public string Name6 { get; set; }
            public string Name7 { get; set; }
            public string Name8 { get; set; }
            public string Name9 { get; set; }
            public string Name10 { get; set; }
            public string Name11 { get; set; }
            public string Name12 { get; set; }
            public string Name13 { get; set; }
            public string Name14 { get; set; }
            public string Name15 { get; set; }
            
        }

        private IEnumerable<string> ReturnArgument(IEnumerable<string> input)
        {
            return input;
        }
        
        private IEnumerable<string> ReturnResult(IEnumerable<string> input)
        {
            var result = input.ToList();
            return result;
        }

    }
    
    
    
}