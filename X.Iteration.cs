using System.Collections.Generic;
using System.Linq;

namespace QuantTC
{
    public static partial class X
    {
        /// <summary>
        /// Take the range of [left, right) (default from left-end)
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="reversed">false: left to right; true right to left</param>
        public static IEnumerable<int> Range(int left, int right, bool reversed = false) =>
            reversed ? RangeR(left, right) : RangeL(left, right);

        /// <summary>
        /// Take the range of [left, right) from the left-end
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <seealso cref="Range"/>
        /// <seealso cref="RangeR"/>
        public static IEnumerable<int> RangeL(int left, int right, int step = 1)
        {
            for (var i = left; i < right; i += step)
            {
                yield return i;
            }
        }

        /// <summary>
        /// Take the range of [left, right) from the right-end
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <seealso cref="Range"/>
        /// <seealso cref="RangeL"/>
        public static IEnumerable<int> RangeR(int left, int right, int step = 1)
        {
            for (var i = right - 1; i >= left; i -= step)
            {
                yield return i;
            }
        }

        /// <summary>
        /// Take the range of [This.Item1, This.Item2)
        /// </summary>
        /// <param name="This">a tuple of (left, right)</param>
        /// <param name="reversed">false: left to right; true right to left</param>
        /// <seealso cref="Range"/>
        public static IEnumerable<int> AsRange(this (int, int) This, bool reversed = false) =>
            Range(This.Item1, This.Item2, reversed);

        /// <summary>
        /// Take the range of [This.Item1, This.Item2) from left-end
        /// </summary>
        /// <param name="This">a tuple of (left, right)</param>
        /// <seealso cref="Range"/>
        public static IEnumerable<int> AsRangeL(this (int, int) This) =>
            RangeL(This.Item1, This.Item2);

        /// <summary>
        /// Take the range of [This.Item1, This.Item2) from right-end
        /// </summary>
        /// <param name="This">a tuple of (left, right)</param>
        /// <seealso cref="Range"/>
        public static IEnumerable<int> AsRangeR(this (int, int) This) =>
            RangeR(This.Item1, This.Item2);

        /// <summary>
        /// Take the range of [left, right) with corresponding This[i]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="reversed">false: left to right; true right to left</param>
        /// <returns></returns>
        public static IEnumerable<(T, int)> Range<T>(this IReadOnlyList<T> This, int left, int right,
            bool reversed = false) =>
            Range(left, right, reversed).Select(i => (This[i], i));

        /// <summary>
        /// Take the range of [left, right) from left-end with corresponding This[i]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static IEnumerable<(T, int)> RangeL<T>(this IReadOnlyList<T> This, int left, int right) =>
            RangeL(left, right).Select(i => (This[i], i));

        /// <summary>
        /// Take the range of [left, right) from right-end with corresponding This[i]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static IEnumerable<(T, int)> RangeR<T>(this IReadOnlyList<T> This, int left, int right) =>
            RangeR(left, right).Select(i => (This[i], i));

        /// <summary>
        /// Take the range of [left, This.Count) from left-end with corresponding This[i]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        public static IEnumerable<(T, int)> FromL<T>(this IReadOnlyList<T> This, int left) =>
            This.RangeL(left, This.Count);

        /// <summary>
        /// Take the range of [0, right) from right-end with corresponding This[i]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static IEnumerable<(T, int)> FromR<T>(this IReadOnlyList<T> This, int right) =>
            This.RangeR(0, right);

        /// <summary>
        /// Take the range of [0, This.Count) from right-end with corresponding This[i]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <returns></returns>
        public static IEnumerable<T> Reverse<T>(this IReadOnlyList<T> This) =>
            RangeR(0, This.Count).Select(i => This[i]);

        /// <summary>
        /// Take the range of [0, This.Count) from right-end with corresponding This[i]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <returns></returns>
        public static IEnumerable<(T, int)> ReverseX<T>(this IReadOnlyList<T> This) =>
            This.RangeR(0, This.Count);
    }
}