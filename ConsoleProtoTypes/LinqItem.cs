using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleProtoTypes
{
    public class LinqItem
    {
        public string Name { get; set; }
        //public IEnumerable<string> Capabilities { get; set; }
        public IEnumerable<Capability> Capabilities { get; set; }

        //public static async ValueTask<IAsyncEnumerable<Capability>> GetCapsAsync(string name)
        //{
        //    return await Task.Run(() => GetLinqItemsWithCaps()
        //       .Where(x => x.Name == name)
        //       .FirstOrDefault().Capabilities
        //       .ToAsyncEnumerable()
        //         );
        //}

        public static async Task<IEnumerable<Capability>> GetCapsAsync2(string name)
        {
            return await Task.Run(() => GetLinqItemsWithCaps()
               .Where(x => x.Name == name)
               .FirstOrDefault().Capabilities
                 );
        }

        public static IEnumerable<Capability> GetCaps(string name)
        {
            return GetLinqItemsWithCaps()
                .Where(x => x.Name == name)
                .FirstOrDefault().Capabilities;
        }

        public static IEnumerable<LinqItem> GetLinqItemsWithCaps()
        {
            return new List<LinqItem>
            {
                new LinqItem
                {
                   Name = "1",
                   Capabilities = new [] { new Capability(1, "Cap1"), new Capability(2, "Cap2") }
                },
                new LinqItem
                {
                   Name = "2",
                   Capabilities =  new [] { new Capability(3, "Cap3"), new Capability(4, "Cap4") }
                },
                 new LinqItem
                {
                   Name = "3",
                   Capabilities = new [] { new Capability(4, "Cap4"), new Capability(5, "Cap5") }
                }
            };
        }
    }

    public class Capability
    {
        public Capability(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}