using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace QuantTC
{
    public static partial class X
    {
        /// <summary>
        /// An alias of foreach
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var obj in source)
            {
                action(obj);
            }
        }

        /// <summary>
        /// An alias of foreach
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var idx = 0;
            foreach (var obj in source)
            {
                action(obj, idx);
                idx++;
            }
        }

        /// <summary>
        /// An alias of foreach
        /// </summary>
        public static void ForEach<T>(this IReadOnlyList<T> source, Action<T, int> action)
        {
            var cnt = source.Count;
            for (var i = 0; i < cnt; i++)
            {
                action(source[i], i);
            }
        }


        /// <summary>
        /// tuple (element, counter), counter started from 0
        /// </summary>
        public static IEnumerable<Tuple<T, int>> WithCounter<T>(this IEnumerable<T> source)
        {
            var counter = 0;
            foreach (var obj in source)
            {
                yield return Tuple.Create(obj, counter);
                counter++;
            }
        }

        /// <summary>
        /// IReadOnlyList's Last (Performance Optimized)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Last<T>(this IReadOnlyList<T> source) => source[source.Count - 1];

        /// <summary>
        /// IReadOnlyList's LastOrDefault (Performance Optimized)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T LastOrDefault<T>(this IReadOnlyList<T> source) =>
            source.Count > 0 ? source[source.Count - 1] : default(T);

        /// <summary>
        /// Filter with counter
        /// </summary>
        public static IEnumerable<Tuple<T, int>> KVWhere<T>(this IEnumerable<T> source,
            Func<Tuple<T, int>, bool> predicate) =>
            source.WithCounter().Where(predicate);

        /// <summary>
        /// Similar to Where but output its index (counter/position)
        /// </summary>
        public static IEnumerable<int> Find<T>(this IEnumerable<T> source, Func<Tuple<T, int>, bool> predicate) =>
            source.KVWhere(predicate).Select(t => t.Item2);

        /// <summary>
        /// get near pairs tuple (prev, next)
        /// </summary>
        /// <remarks>source.Count() == source.NearPairs() + 1</remarks>
        public static IEnumerable<Tuple<T, T>> NearPairs<T>(this IEnumerable<T> source) => source.MovingPairs().Skip(1);

        private static IEnumerable<Tuple<T, T>> MovingPairs<T>(this IEnumerable<T> source)
        {
            var last = default(T);
            foreach (var obj in source)
            {
                yield return Tuple.Create(last, obj);
                last = obj;
            }
        }

        /// <summary>
        /// Fill the range [left, right) in List
        /// </summary>
        /// <typeparam name="T">List Element Type</typeparam>
        /// <param name="source">List Container</param>
        /// <param name="left">Left Index</param>
        /// <param name="right">Right Index</param>
        /// <param name="elementFunc">For each index, returning an instance of type T</param>
        public static void FillRange<T>(this List<T> source, int left, int right, Func<int, T> elementFunc)
        {
            Range(left, right).ForEach(i =>
            {
                if (i < source.Count)
                {
                    source[i] = elementFunc(i);
                }
                else
                {
                    source.Add(elementFunc(i));
                }
            });
        }

        public static int MaxWith(this int This, int value) => Math.Max(This, value);
        public static int MinWith(this int This, int value) => Math.Min(This, value);
        public static int RoundTo(this int value, int left, int right) => value.MaxWith(left).MinWith(right);

        /// <summary>
        /// Get the countdown from the N-th element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="n">(0, This.Count]</param>
        /// <returns>This[This.Count - n]</returns>
        public static T Countdown<T>(this IReadOnlyList<T> This, int n) => This[This.Count - n];

        /// <summary>
        /// Judge if a value is same signed with another
        /// </summary>
        public static bool IsSameSigned(this double subject, double @object) =>
            subject >= 0 && @object >= 0 || subject < 0 && @object < 0;

        /// <summary>
        /// Judge if a value is same signed with another
        /// </summary>
        public static bool IsSameSigned(this int subject, int @object) =>
            subject > 0 && @object > 0 || subject < 0 && @object < 0 || subject == 0 && @object == 0;

        /// <summary>
        /// Judge if a value is different signed with another
        /// </summary>
        public static bool IsDiffSigned(this double subject, double @object) =>
            subject >= 0 && @object < 0 || subject < 0 && @object >= 0;

        /// <summary>
        /// Judge if a value is different signed with another
        /// </summary>
        public static bool IsDiffSigned(this int subject, int @object) => !subject.IsSameSigned(@object);

        /// <summary>
        /// Action To Func
        /// </summary>
        /// <typeparam name="T">class or ref type</typeparam>
        /// <returns>The Function returning the origin object</returns>
        public static Func<T, T> ToFunc<T>(this Action<T> action) => t =>
        {
            action(t);
            return t;
        };

        public static int Abs(this int v) => Math.Abs(v);
        public static double Abs(this double v) => Math.Abs(v);

        /// <summary>
        /// Judge if a value is between l and u : [l, u]
        /// </summary>
        /// <param name="v"></param>
        /// <param name="l">Lower Bound</param>
        /// <param name="u">Upper Bound</param>
        /// <returns></returns>
        public static bool IsBetween(this double v, double l, double u) => l <= v && v <= u;

        /// <summary>
        /// Judge if a value is between l and u : [l, u]
        /// </summary>
        /// <param name="v"></param>
        /// <param name="l">Lower Bound</param>
        /// <param name="u">Upper Bound</param>
        /// <returns></returns>
        [Pure]
        public static bool IsBetween<T>(this IComparable<T> v, T l, T u) => v.CompareTo(l) >= 0 && v.CompareTo(u) <= 0;

        /// <summary>
        /// Used in Indicators' Count Sync
        /// </summary>
        /// <param name="ints">Indicators' count</param>
        /// <returns>minimum count</returns>
        public static int Min(params int[] ints) => ints.Min();

        /// <summary>
        /// Linear Scaled Euclidean Distance:
        /// Math.Sqrt(Math.Pow((x1 - x2) * scaleX, 2) + Math.Pow((y1 - y2) * scaleY, 2))
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        /// <returns></returns>
        public static double ScaledDistance(int x1, double y1, int x2, double y2, double scaleX, double scaleY) =>
            Math.Sqrt(Math.Pow((x1 - x2) * scaleX, 2) + Math.Pow((y1 - y2) * scaleY, 2));

        /// <summary>
        /// Linear Scaled Slope:
        /// ((y2 - y1) * scaleY) / ((x2 - x1) * scaleX)
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        /// <returns></returns>
        public static double ScaledSlope(int x1, double y1, int x2, double y2, double scaleX, double scaleY) =>
            ((y2 - y1) * scaleY) / ((x2 - x1) * scaleX);

        /// <summary>
        /// Cast deg to arc
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static double ToArc(this double deg) => deg * Math.PI / 180.0;

        /// <summary>
        /// Cast arc to deg
        /// </summary>
        /// <param name="arc"></param>
        /// <returns></returns>
        public static double ToDeg(this double arc) => arc * 180.0 / Math.PI;

        public static TR Apply<TT, TR>(this TT This, Func<TT, TR> func) => func(This);
        public static TR Apply<TT, T1, TR>(this TT This, Func<TT, T1, TR> func, T1 arg1) => func(This, arg1);

        public static TR Apply<TT, T1, T2, TR>(this TT This, Func<TT, T1, T2, TR> func, T1 arg1, T2 arg2) =>
            func(This, arg1, arg2);

        public static TR Apply<TT, T1, T2, T3, TR>(this TT This, Func<TT, T1, T2, T3, TR> func, T1 arg1, T2 arg2,
            T3 arg3) => func(This, arg1, arg2, arg3);

        public static TR Apply<TT, T1, T2, T3, T4, TR>(this TT This, Func<TT, T1, T2, T3, T4, TR> func, T1 arg1,
            T2 arg2, T3 arg3, T4 arg4) => func(This, arg1, arg2, arg3, arg4);

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