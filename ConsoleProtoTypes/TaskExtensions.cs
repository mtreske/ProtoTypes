using System;
using System.Threading.Tasks;

namespace ConsoleProtoTypes
{
    static class LTaskExtensions
    {
        public static async Task<TResult> Select<TSource, TResult>(
            this Task<TSource> source, Func<TSource, TResult> selector)
        {
            var sourceResult = await source;
            if (sourceResult == null)
                return default(TResult);
            return selector.Invoke(sourceResult);
        }

        public static async Task<TResult> SelectMany<TSource, TResult>(this Task<TSource> source,
            Func<TSource, Task<TResult>> selector)
        {
            var sourceResult = await source;
            if (sourceResult == null)
                return default(TResult);
            return await selector.Invoke(sourceResult);
        }
    }
}