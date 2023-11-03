using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace VariousTests
{
    public class YieldTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var values = Enumerable.Empty<int>().ToList();
            for (int i = 0; i < 10; i++)
            {
                values.Add(i);
            }

            var result = GetValues(values).ToList();
        }

        private IEnumerable<int> GetValues(IEnumerable<int> values)
        {
            yield break;
            foreach (var value in values)
            {
                yield return value;
            }
        }

        [Test]
        public void Testrun()
        {
            var input = "a====10;b=2";

            var inputArray = input.Split(';');

            var key = inputArray[0];
            var value = inputArray.TakeLast(1).FirstOrDefault();

            var count = input.Where(x => x.Equals('=')).Count();

            for (int i = 0; i < count-1; i++)
            {
                value = "=" + value;
            }


        }
    }
}