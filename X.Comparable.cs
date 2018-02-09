using System;
using System.Collections.Generic;

namespace QuantTC
{
    public static partial class X
    {
        // Principles
        public static bool IsUX<T>(this (T, T) This, IComparable<T> that) =>
            that.CompareTo(This.Item1) >= 0 && that.CompareTo(This.Item2) < 0;

        public static bool IsDX<T>(this (T, T) This, IComparable<T> that) =>
            that.CompareTo(This.Item1) <= 0 && that.CompareTo(This.Item2) > 0;

        public static bool IsUX<T>(this (T, T) This, (IComparable<T>, IComparable<T>) that) =>
            that.Item1.CompareTo(This.Item1) >= 0 && that.Item2.CompareTo(This.Item2) < 0;

        public static bool IsDX<T>(this (T, T) This, (IComparable<T>, IComparable<T>) that) =>
            that.Item1.CompareTo(This.Item1) <= 0 && that.Item2.CompareTo(This.Item2) > 0;

        // Extensions
        public static bool IsUXAt<T>(this IReadOnlyList<T> This, int i, IComparable<T> that) =>
            IsUX(This.NearPairAt(i), that);

        public static bool IsDXAt<T>(this IReadOnlyList<T> This, int i, IComparable<T> that) =>
            IsDX(This.NearPairAt(i), that);

        public static bool IsUXAt<T>(this IReadOnlyList<T> This, IReadOnlyList<IComparable<T>> that, int i) =>
            This.NearPairAt(i).IsUX(that.NearPairAt(i));

        public static bool IsDXAt<T>(this IReadOnlyList<T> This, IReadOnlyList<IComparable<T>> that, int i) =>
            This.NearPairAt(i).IsDX(that.NearPairAt(i));

        public static bool IsUX<T>(this IReadOnlyList<T> This, IReadOnlyList<IComparable<T>> that) =>
            This.IsUXAt(that, This.Count - 1);

        public static bool IsDX<T>(this IReadOnlyList<T> This, IReadOnlyList<IComparable<T>> that) =>
            This.IsDXAt(that, This.Count - 1);

        // Principles
        public static bool IsUpX<T>(this (IComparable<T>, IComparable<T>) This, T that) =>
            This.Item1.CompareTo(that) <= 0 && This.Item2.CompareTo(that) > 0;

        public static bool IsDownX<T>(this (IComparable<T>, IComparable<T>) This, T that) =>
            This.Item1.CompareTo(that) >= 0 && This.Item2.CompareTo(that) < 0;

        public static bool IsUpX<T>(this (IComparable<T>, IComparable<T>) This, (T, T) that) =>
            This.Item1.CompareTo(that.Item1) <= 0 && This.Item2.CompareTo(that.Item2) > 0;

        public static bool IsDownX<T>(this (IComparable<T>, IComparable<T>) This, (T, T) that) =>
            This.Item1.CompareTo(that.Item1) >= 0 && This.Item2.CompareTo(that.Item2) < 0;

        // Extensions
        public static bool IsUpXAt<T>(this IReadOnlyList<IComparable<T>> This, int i, T that) =>
            IsUpX(This.NearPairAt(i), that);

        public static bool IsDownXAt<T>(this IReadOnlyList<IComparable<T>> This, int i, T that) =>
            IsDownX(This.NearPairAt(i), that);

        public static bool IsUpXAt<T>(this IReadOnlyList<IComparable<T>> This, IReadOnlyList<T> that, int i) =>
            This.NearPairAt(i).IsUpX(that.NearPairAt(i));

        public static bool IsDownXAt<T>(this IReadOnlyList<IComparable<T>> This, IReadOnlyList<T> that, int i) =>
            This.NearPairAt(i).IsDownX(that.NearPairAt(i));

        public static bool IsUpX<T>(this IReadOnlyList<IComparable<T>> This, IReadOnlyList<T> that) =>
            This.IsUpXAt(that, This.Count - 1);

        public static bool IsDownX<T>(this IReadOnlyList<IComparable<T>> This, IReadOnlyList<T> that) =>
            This.IsDownXAt(that, This.Count - 1);

        #region Deprecated
        [Obsolete("Using ValueTuple instand")]
        public static bool IsUpX(this Tuple<double, double> subject, Tuple<double, double> @object) =>
            subject.Item1 <= @object.Item1 && subject.Item2 > @object.Item2;
        /// <summary>
        /// Judge if a value tuple become less than another
        /// </summary>
        [Obsolete("Using ValueTuple instand")]
        public static bool IsDownX(this Tuple<double, double> subject, Tuple<double, double> @object) =>
            subject.Item1 >= @object.Item1 && subject.Item2 < @object.Item2;

        /// <summary>
        /// Judge if a value tuple become less than a contain value
        /// </summary>
        [Obsolete("Using ValueTuple instand")]
        public static bool IsDownX(this Tuple<double, double> subject, double value) =>
            subject.Item1 >= value && subject.Item2 < value;
        #endregion
    }
}