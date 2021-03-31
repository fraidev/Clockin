using System.Collections.Generic;
using System.Linq;

namespace Clockin.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T[]> GetSlices<T>(this IEnumerable<T> source, int n)
        {
            if (n <= 0)
                n = 1;
            var it = source;
            var enumerable = it.ToList();
            var slice = enumerable.Take(n).ToArray();
            it = enumerable.Skip(n);
            while (slice.Length != 0)
            {
                yield return slice;
                slice = it.Take(n).ToArray();
                it = it.Skip(n);
            }                           
        } 
    }
}