using System;
using System.Collections.Generic;
using System.Text;

namespace QuantTC
{
    public static partial class X
    {
        /// <summary>
        /// VT(src[idx - 1], src[idx])
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Tuple<T, T> NearPairAt<T>(this IReadOnlyList<T> source, int index) =>
            Tuple.Create(source[index - 1], source[index]);

        public static IEnumerable<int> ToRange(this Tuple<int, int> range) => Functions.Range(range.Item1, range.Item2);

        public static IEnumerable<int> ToRangeRight(this Tuple<int, int> range) =>
            Functions.RangeRight(range.Item1, range.Item2);
    }
}
