using System;
using System.Collections.Generic;
using System.Linq;
using QuantTC.Indicators.Generic;

namespace QuantTC.Data
{
    /// <summary>
    /// the Price Datum with Open Price
    /// </summary>
    public interface IPriceO
    {
        /// <summary>
        /// Open Price
        /// </summary>
        double Open { get; }
    }

    /// <summary>
    /// the Price Datum with High Price
    /// </summary>
    public interface IPriceH
    {
        /// <summary>
        /// High Price
        /// </summary>
        double High { get; }
    }

    /// <summary>
    /// the Price Datum with Low Price
    /// </summary>
    public interface IPriceL
    {
        /// <summary>
        /// Low Price
        /// </summary>
        double Low { get; }
    }

    /// <summary>
    /// the Price Datum with Close Price
    /// </summary>
    public interface IPriceC
    {
        /// <summary>
        /// Close Price
        /// </summary>
        double Close { get; }
    }

    /// <inheritdoc cref="IPriceO" />
    /// <inheritdoc cref="IPriceH" />
    /// <summary>
    /// the Price Datum with Open and High Price
    /// </summary>
    public interface IPriceOH : IPriceO, IPriceH
    {
    }

    /// <inheritdoc cref="IPriceO" />
    /// <inheritdoc cref="IPriceL" />
    /// <summary>
    /// the Price Datum with Open and Low Price
    /// </summary>
    public interface IPriceOL : IPriceO, IPriceL
    {
    }

    /// <inheritdoc cref="IPriceO" />
    /// <inheritdoc cref="IPriceC" />
    /// <summary>
    /// the Price Datum with Open and Close Price
    /// </summary>
    public interface IPriceOC : IPriceO, IPriceC
    {
    }

    /// <inheritdoc cref="IPriceH" />
    /// <inheritdoc cref="IPriceL" />
    /// <summary>
    /// the Price Datum with High and Low Price
    /// </summary>
    public interface IPriceHL : IPriceH, IPriceL
    {
    }

    /// <inheritdoc cref="IPriceH" />
    /// <inheritdoc cref="IPriceC" />
    /// <summary>
    /// the Price Datum with High and Close Price
    /// </summary>
    public interface IPriceHC : IPriceH, IPriceC
    {
    }

    /// <inheritdoc cref="IPriceL" />
    /// <inheritdoc cref="IPriceC" />
    /// <summary>
    /// the Price Datum with Low and Close Price
    /// </summary>
    public interface IPriceLC : IPriceL, IPriceC
    {
    }

    /// <inheritdoc cref="IPriceOH" />
    /// <inheritdoc cref="IPriceOL" />
    /// <inheritdoc cref="IPriceHL" />
    /// <summary>
    /// the Price Datum with Open, High and Low Price
    /// </summary>
    public interface IPriceOHL : IPriceOH, IPriceOL, IPriceHL
    {
    }

    /// <inheritdoc cref="IPriceOC" />
    /// <inheritdoc cref="IPriceOH" />
    /// <inheritdoc cref="IPriceHC" />
    /// <summary>
    /// the Price Datum with Open, High and Close Price
    /// </summary>
    public interface IPriceOHC : IPriceOH, IPriceOC, IPriceHC
    {
    }

    /// <inheritdoc cref="IPriceOL" />
    /// <inheritdoc cref="IPriceOC" />
    /// <inheritdoc cref="IPriceLC" />
    /// <summary>
    /// the Price Datum with Open, Low and Close Price
    /// </summary>
    public interface IPriceOLC : IPriceOL, IPriceOC, IPriceLC
    {
    }

    /// <inheritdoc cref="IPriceHL" />
    /// <inheritdoc cref="IPriceHC" />
    /// <inheritdoc cref="IPriceLC" />
    /// <summary>
    /// the Price Datum with High, Low and Close Price
    /// </summary>
    public interface IPriceHLC : IPriceHL, IPriceHC, IPriceLC
    {
    }

    /// <inheritdoc cref="IPriceOHL" />
    /// <inheritdoc cref="IPriceOHC" />
    /// <inheritdoc cref="IPriceOLC" />
    /// <inheritdoc cref="IPriceHLC" />
    /// <summary>
    /// the Price Datum with Open, High, Low and Close Price
    /// </summary>
    public interface IPriceOHLC : IPriceOHL, IPriceOHC, IPriceOLC, IPriceHLC
    {
    }

    /// <inheritdoc />
    /// <summary>
    /// The Price Datum with Open, High, Low, Close and other items
    /// </summary>
    public interface IPrice : IPriceOHLC
    {
        /// <summary>
        /// DateTime (at start)
        /// </summary>
        DateTime DateTime { get; }
        /// <summary>
        /// Volume (in lots)
        /// </summary>
        int Volume { get; }
        /// <summary>
        /// Open Interest (in lots)
        /// </summary>
        int OpenInterest { get; }
    }

    public static partial class X
    {
        public static bool IsBullish(this IPriceOC bar) => bar.Close > bar.Open;
        public static bool IsBearish(this IPriceOC bar) => bar.Close < bar.Open;

        /// <summary>
        /// the Top of Real Body
        /// </summary>
        /// <param name="bar">candlestick</param>
        /// <returns></returns>
        public static double Top(this IPriceOC bar) => Math.Max(bar.Close, bar.Open);

        /// <summary>
        /// the Bottom of Real Body
        /// </summary>
        /// <param name="bar">candlestick</param>
        /// <returns></returns>
        public static double Bottom(this IPriceOC bar) => Math.Min(bar.Close, bar.Open);

        /// <summary>
        /// the Length of Real Body
        /// </summary>
        /// <param name="bar">candlestick</param>
        /// <returns></returns>
        public static double RealBody(this IPriceOC bar) => Math.Abs(bar.Close - bar.Open);

        public static double UpperShadow(this IPriceOHC price) => price.High - price.Top();
        public static double LowerShadow(this IPriceOLC price) => price.Bottom() - price.Low;

        public static double Body(this IPriceHL price) => price.High - price.Low;

        public static double PriceRange(this IReadOnlyList<IPriceHL> This, int from, int to) =>
            QuantTC.X.Range(from, to).Max(i => This[i].High) - QuantTC.X.Range(from, to).Min(i => This[i].Low);

        public static IIndicator<double> Open(this IIndicator<IPriceO> This) => This.Transform(p => p.Open);
        public static IIndicator<double> High(this IIndicator<IPriceH> This) => This.Transform(p => p.High);
        public static IIndicator<double> Low(this IIndicator<IPriceL> This) => This.Transform(p => p.Low);
        public static IIndicator<double> Close(this IIndicator<IPriceC> This) => This.Transform(p => p.Close);

        public static (double, double) HighClose(this IPriceHC This) => (This.High, This.Close);
        public static (double, double) LowClose(this IPriceLC This) => (This.Low, This.Close);

        public static bool IsIntersect(this IPriceHL This, double that) => that.IsBetween(This.Low, This.High);

        public static bool IsIntersectAt(this IReadOnlyList<IPriceHL> This, int i, double that) =>
            This[i].IsIntersect(that);

        public static bool IsIntersectAt(this IReadOnlyList<IPriceHL> This, IReadOnlyList<double> that, int i) =>
            This[i].IsIntersect(that[i]);

        public static bool IsInsideDownXAt(this IReadOnlyList<IPriceHC> This, IReadOnlyList<double> that, int i) =>
            This[i].HighClose().IsDx(that[i]);

        public static bool IsInsideUpXAt(this IReadOnlyList<IPriceLC> This, IReadOnlyList<double> that, int i) =>
            This[i].LowClose().IsUx(that[i]);

        public static bool IsCloseUpXAt(this IReadOnlyList<IPriceC> This, IReadOnlyList<double> that, int i) =>
            (This[i - 1].Close, This[i].Close).IsUx(that.NearPairAt(i));

        public static bool IsCloseDownXAt(this IReadOnlyList<IPriceC> This, IReadOnlyList<double> that, int i) =>
            (This[i - 1].Close, This[i].Close).IsDx(that.NearPairAt(i));

        public static bool IsLowUpXAt(this IReadOnlyList<IPriceL> This, IReadOnlyList<double> that, int i) =>
            (This[i - 1].Low, This[i].Low).IsUx(that.NearPairAt(i));

        public static bool IsHighDownXAt(this IReadOnlyList<IPriceH> This, IReadOnlyList<double> that, int i) =>
            (This[i - 1].High, This[i].High).IsDx(that.NearPairAt(i));

        /// <summary>
        /// Price down crossed in any way
        /// </summary>
        /// <param name="This"></param>
        /// <param name="that"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsDownXAt(this IReadOnlyList<IPriceHC> This, IReadOnlyList<double> that, int i) =>
            This.IsInsideDownXAt(that, i) || This.IsCloseDownXAt(that, i) || This.IsHighDownXAt(that, i);

        /// <summary>
        /// Price up crossed in any way
        /// </summary>
        /// <param name="This"></param>
        /// <param name="that"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsUpXAt(this IReadOnlyList<IPriceLC> This, IReadOnlyList<double> that, int i) =>
            This.IsInsideUpXAt(that, i) || This.IsCloseUpXAt(that, i) || This.IsLowUpXAt(that, i);
    }
}
