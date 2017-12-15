using System;
using System.Collections.Generic;
using System.Text;

namespace QuantTC
{
    public static class X
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

        public static TR Apply<T1, TR>(this T1 arg, Func<T1, TR> func) => func(arg);
        public static TR Apply<T1, T2, TR>(this T1 arg, Func<T1, T2, TR> func, T2 arg2) => func(arg, arg2);

        public static IEnumerable<int> ToRange(this Tuple<int, int> range) => Functions.Range(range.Item1, range.Item2);

        public static IEnumerable<int> ToRangeRight(this Tuple<int, int> range) =>
            Functions.RangeRight(range.Item1, range.Item2);
    }
}
