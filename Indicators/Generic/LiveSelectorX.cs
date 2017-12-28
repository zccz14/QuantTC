using System;

namespace QuantTC.Indicators.Generic
{
    /// <summary>
    /// Live Selector Extension
    /// </summary>
    public static class LiveSelectorX
    {
        /// <summary>
        /// Build a live selector
        /// </summary>
        /// <typeparam name="T1">Source Type</typeparam>
        /// <typeparam name="T2">Target Type</typeparam>
        /// <param name="source">Source Indicator</param>
        /// <param name="title">Title</param>
        /// <param name="selector">Select Function</param>
        public static LiveSelector<T1, T2> LiveSelect<T1, T2>(this IIndicator<T1> source, string title,
            Func<T1, int, T2> selector) =>
            new LiveSelector<T1, T2>(source, selector) {Title = title};

        /// <summary>
        /// Build a live selector
        /// </summary>
        /// <typeparam name="T1">Source Type</typeparam>
        /// <typeparam name="T2">Target Type</typeparam>
        /// <param name="source">Source Indicator</param>
        /// <param name="selector">Select Function</param>
        public static LiveSelector<T1, T2> LiveSelect<T1, T2>(this IIndicator<T1> source, Func<T1, int, T2> selector) =>
            new LiveSelector<T1, T2>(source, selector);
    }
}