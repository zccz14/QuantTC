using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuantTC.Indicators.Generic
{
    /// <inheritdoc />
    /// <summary>
    /// Tracking a quantity automatically when timer updated.
    /// </summary>
    /// <typeparam name="T">Quantity Type</typeparam>
    /// <remarks>
    /// There's no granted that the count is equal to the timer.
    /// </remarks>
    public class Tracking<T>: Indicator<T>
    {
        /// <inheritdoc />
        public Tracking(IIndicator timer, Func<T> getter)
        {
            Timer = timer;
            Getter = getter;
            Timer.Update += Timer_Update;
        }

        private void Timer_Update()
        {
            Data.Add(Getter());
            FollowUp();
        }

        private IIndicator Timer { get; }
        private Func<T> Getter { get; }
    }

    public static partial class X
    {
        /// <summary>
        /// Create a tracking instance
        /// </summary>
        /// <typeparam name="T">Quantity Type</typeparam>
        /// <param name="This">Update Timer</param>
        /// <param name="getter"></param>
        /// <returns></returns>
        public static Tracking<T> Track<T>(this IIndicator This, Func<T> getter) =>
            new Tracking<T>(This, getter);

        /// <summary>
        /// Create a tracking instance, shortcut for This.Track(that.LastOrDefault)
        /// </summary>
        /// <typeparam name="T">Quantity Type</typeparam>
        /// <param name="This">Update Timer</param>
        /// <param name="that"></param>
        /// <returns></returns>
        public static Tracking<T> Track<T>(this IIndicator This, IReadOnlyList<T> that) =>
            This.Track(that.LastOrDefault);
    }
}
