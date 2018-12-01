using System.Collections.Generic;

namespace ZipSample
{
    static class MyLinq
    {
        public static IEnumerable<TSource> MyConcat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            var firstEnumerator = first.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                yield return firstEnumerator.Current;
            }

            var secondEnumerator = second.GetEnumerator();
            while (secondEnumerator.MoveNext())
            {
                yield return secondEnumerator.Current;
            }
        }

    }
}