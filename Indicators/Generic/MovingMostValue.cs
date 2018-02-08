using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantTC.Indicators.Generic
{
    /// <inheritdoc />
    /// <summary>
    /// Moving Most Value (Optimized for long period)
    /// </summary>
    /// <remarks>
    /// Each online updating costs O(log P) time (cutdown from O(P))
    /// </remarks>
    public class MovingMostValue<T> : Indicator<T>
    {
        /// <inheritdoc />
        public MovingMostValue(IIndicator<T> source, int period, Comparison<T> comparison)
        {
            Source = source;
            Period = period;
            Counter = new SortedDictionary<T, int>(Comparer<T>.Create(comparison));
            Source.Update += SourceOnUpdate;
        }

        private void SourceOnUpdate()
        {
            Data.FillRange(Count, Source.Count, Calc);
            FollowUp();
        }

        private T Calc(int i)
        {
            var e = Source[i]; // the element to add
            if (!Counter.ContainsKey(e))
            {
                Counter.Add(e, 0);
            }

            Counter[e]++;
            if (i >= Period)
            {
                var r = Source[i - Period]; // the element to remove
                if (Counter[r] <= 1)
                {
                    Counter.Remove(r);
                }
                else
                {
                    Counter[r]--;
                }
            }

            return Counter.First().Key;
        }

        private IIndicator<T> Source { get; }
        private int Period { get; }
        private SortedDictionary<T, int> Counter { get; }
    }

    public static partial class X
    {
        /// <summary>
        /// Moving Most Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="period"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static MovingMostValue<T> MMV<T>(this IIndicator<T> This, int period, Comparison<T> comparison) =>
            new MovingMostValue<T>(This, period, comparison);

        /// <summary>
        /// Highest Value
        /// </summary>
        /// <param name="This"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static MovingMostValue<double> HighestValue(this IIndicator<double> This, int period) =>
            This.MMV(period, (x, y) => y.CompareTo(x));

        /// <summary>
        /// Lowest Value
        /// </summary>
        /// <param name="This"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static MovingMostValue<double> LowestValue(this IIndicator<double> This, int period) =>
            This.MMV(period, (x, y) => x.CompareTo(y));
    }
}
