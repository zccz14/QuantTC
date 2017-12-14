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
    }
}
