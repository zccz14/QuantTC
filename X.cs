using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantTC
{
    public static partial class X
    {
        /// <summary>
        /// short cut for (This[i - 1], This[i])
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static (T, T) NearPairAt<T>(this IReadOnlyList<T> This, int i) => (This[i - 1], This[i]);

        public static T MaxAt<T>(this IEnumerable<T> This, Func<T, double> val)
        {
            var enumerable = This as T[] ?? This.ToArray();
            var t = enumerable.FirstOrDefault();
            foreach (var e in enumerable)
            {
                if (val(t) < val(e))
                {
                    t = e;
                }
            }
            return t;
        }
        public static T MinAt<T>(this IEnumerable<T> This, Func<T, double> val)
        {
            var enumerable = This as T[] ?? This.ToArray();
            var t = enumerable.FirstOrDefault();
            foreach (var e in enumerable)
            {
                if (val(t) > val(e))
                {
                    t = e;
                }
            }
            return t;
        }
    }
}