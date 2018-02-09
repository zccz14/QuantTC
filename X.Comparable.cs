using System;
using System.Collections.Generic;

namespace QuantTC
{
    public static partial class X
    {
        /// <summary>
        /// Is Upward Cross: This.Item1 &lt;= that &lt; This.Item2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="that"></param>
        /// <returns></returns>
        public static bool IsUx<T>(this (IComparable<T>, IComparable<T>) This, T that) =>
            This.Item1.CompareTo(that) <= 0 && This.Item2.CompareTo(that) > 0;

        /// <summary>
        /// Is Downward Cross: This.Item1 &gt;= that &gt; This.Item2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="that"></param>
        /// <returns></returns>
        public static bool IsDx<T>(this (IComparable<T>, IComparable<T>) This, T that) =>
            This.Item1.CompareTo(that) >= 0 && This.Item2.CompareTo(that) < 0;

        /// <summary>
        /// Is Upward Cross: This.Item1 &lt;= that.Item1 and that.Item2 &lt; This.Item2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="that"></param>
        /// <returns></returns>
        public static bool IsUx<T>(this (IComparable<T>, IComparable<T>) This, (T, T) that) =>
            This.Item1.CompareTo(that.Item1) <= 0 && This.Item2.CompareTo(that.Item2) > 0;

        /// <summary>
        /// Is Downward Cross: This.Item1 &gt;= that.Item1 and that.Item2 &gt; This.Item2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="that"></param>
        /// <returns></returns>
        public static bool IsDx<T>(this (IComparable<T>, IComparable<T>) This, (T, T) that) =>
            This.Item1.CompareTo(that.Item1) >= 0 && This.Item2.CompareTo(that.Item2) < 0;

        /// <summary>
        /// Is Upward Cross: (This[i - 1], This[i]) Ux that
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="This"></param>
        /// <param name="that"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <seealso>
        ///     <cref>IsUx</cref>
        /// </seealso>
        public static bool IsUxAt<T1, T2>(this IReadOnlyList<T1> This, T2 that, int i)
            where T1 : IComparable<T2> => This.NearPairAt(i).IsUx(that);

        /// <summary>
        /// Is Downward Cross: (This[i - 1], This[i]) Dx that
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="This"></param>
        /// <param name="that"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <seealso>
        ///     <cref>IsDx</cref>
        /// </seealso>
        public static bool IsDxAt<T1, T2>(this IReadOnlyList<T1> This, T2 that, int i)
            where T1 : IComparable<T2> => This.NearPairAt(i).IsDx(that);

        /// <summary>
        /// Is Upward Cross: (This[i - 1], This[i]) Ux (that[i - 1], that[i])
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="This"></param>
        /// <param name="that"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <seealso>
        ///     <cref>IsDx</cref>
        /// </seealso>
        public static bool IsUxAt<T1, T2>(this IReadOnlyList<T1> This, IReadOnlyList<T2> that, int i)
            where T1 : IComparable<T2> => This.NearPairAt(i).IsUx(that.NearPairAt(i));

        /// <summary>
        /// Is Downward Cross: (This[i - 1], This[i]) Dx (that[i - 1], that[i])
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="This"></param>
        /// <param name="that"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <seealso>
        ///     <cref>IsDx</cref>
        /// </seealso>
        public static bool IsDxAt<T1, T2>(this IReadOnlyList<T1> This, IReadOnlyList<T2> that, int i)
            where T1 : IComparable<T2> => This.NearPairAt(i).IsDx(that.NearPairAt(i));
    }
}