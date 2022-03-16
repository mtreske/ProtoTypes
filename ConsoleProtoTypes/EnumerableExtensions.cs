//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ConsoleProtoTypes
//{
//    public static class EnumerableExtensions
//    {
//        public static async Task<IDictionary<TKey, TValue>> ToDictionaryAsync<TInput, TKey, TValue>(
//           this IEnumerable<TInput> enumerable,
//           Func<TInput, TKey> syncKeySelector,
//           Func<TInput, Task<TValue>> asyncValueSelector)
//        {
//            var dict = new Dictionary<TKey, TValue>();

//            foreach (var input in enumerable)
//            {
//                dict.Add(syncKeySelector(input), await asyncValueSelector(input));
//            }

//            return dict;
//        }

//        public static async Task<ILookup<TKey, TElement>> ToLookupAsync<TSource, TKey, TElement>(
//            this IEnumerable<TSource> source, 
//            Func<TSource, TKey> syncKeySelector, 
//            Func<TSource, Task<TElement>> asyncElementSelector)
//        {
//            var list = new List<(TKey, TElement)>();

//            foreach (var item in source)
//            {
//                list.Add( (syncKeySelector(item),  await asyncElementSelector(item)) );
//            }    
            
//            return list.ToLookup(_ => _.Item1, _ => _.Item2); 
//        }

//        public static async Task<IEnumerable<TValue>> ToListAsync<TValue>(this IEnumerable<Task<TValue>> enumerable)
//        {
//            var result = new List<TValue>();
//            foreach (var asyncfunc in enumerable)
//            {
//                result.Add(await asyncfunc);
//            }

//            return result;
//        }

//        //public static IEnumerable<TResult> SelectManyAsync<TSource, TCollection, TResult>(
//        //    this IEnumerable<TSource> source, 
//        //    Func<TSource, IEnumerable<TCollection>> syncCollectionSelector, 
//        //    Func<TSource, TCollection, TResult> asyncResultSelector)
//        //{ 
            
//        //}
//    }
//}