using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

        [Test]
        public void MoveNext_Foces_Materialization()
        {
            var mem1 = GC.GetTotalMemory(true);
            var itemsList = GetItemList(100000).ToList();
            var mem2= GC.GetTotalMemory(true);
            var diff1 = mem2 - mem1;
            var items = itemsList.Select(x => x);//GetItemList(100000).ToList();
            var mem3= GC.GetTotalMemory(true);
            
            items.Count();
            var mem4= GC.GetTotalMemory(true);
            items.Count();
            items.Count();
            items.Count();
            items.Count();

            var mem5= GC.GetTotalMemory(true);
            var itemsList2 = itemsList.ToList();
            var mem6= GC.GetTotalMemory(true);
            var diff2 = mem6 - mem5;
            
            var mem7= GC.GetTotalMemory(true);
            var itemsList3 = itemsList.ToImmutableList();
            var mem8= GC.GetTotalMemory(true);
            var diff3 = mem8 - mem7;
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