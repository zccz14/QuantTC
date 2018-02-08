using System;
using System.Collections.Generic;
using System.Linq;
using QuantTC.Indicators.Generic;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
namespace QuantTC.Data
{
    public interface IPriceHC : IPriceH, IPriceC
    {

    }
    public interface IPriceLC : IPriceL, IPriceC
    {

    }
    public interface IPriceOH : IPriceO, IPriceH
    {

    }
    public interface IPriceOL : IPriceO, IPriceH
    {

    }
    public interface IPriceHL : IPriceH, IPriceL
    {
    }

    public interface IPriceOC : IPriceO, IPriceC
    {
    }

    public interface IPriceOHC : IPriceOC, IPriceHC, IPriceOH
    {
    }

    public interface IPriceOLC : IPriceOC, IPriceLC, IPriceOL
    {

    }

    public interface IPriceOHLC : IPriceOHC, IPriceOLC, IPriceHL
    {

    }

    public interface IPriceHLC: IPriceHL, IPriceHC, IPriceLC { }
    public interface IPriceOHL: IPriceOH, IPriceOL, IPriceHL { }

    public interface IPriceO
    {
        double Open { get; }
    }

    public interface IPriceC
    {
        double Close { get; }
    }

    public interface IPriceH
    {
        double High { get; }
    }

    public interface IPriceL
    {
        double Low { get; }
    }

    public interface IPrice : IPriceOHLC
    {
        DateTime DateTime { get; }
        int Volume { get; }
        int OpenInterest { get; }
    }

    public static class X
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

        public static Tuple<double, double> HighClose(this IPriceHC This) => QuantTC.X.VT(This.High, This.Close);
        public static Tuple<double, double> LowClose(this IPriceLC This) => QuantTC.X.VT(This.Low, This.Close);

        public static bool IsIntersect(this IPriceHL This, double that) => that.IsBetween(This.Low, This.High);

        public static bool IsIntersectAt(this IReadOnlyList<IPriceHL> This, int i, double that) =>
            This[i].IsIntersect(that);

        public static bool IsIntersectAt(this IReadOnlyList<IPriceHL> This, IReadOnlyList<double> that, int i) =>
            This[i].IsIntersect(that[i]);

        public static bool IsInsideDownXAt(this IReadOnlyList<IPriceHC> This, IReadOnlyList<double> that, int i) =>
            This[i].HighClose().IsDownX(that[i]);

        public static bool IsInsideUpXAt(this IReadOnlyList<IPriceLC> This, IReadOnlyList<double> that, int i) =>
            This[i].LowClose().IsUpX(that[i]);

        public static bool IsCloseUpXAt(this IReadOnlyList<IPriceC> This, IReadOnlyList<double> that, int i) =>
            QuantTC.X.VT(This[i - 1].Close, This[i].Close).IsUpX(that.NearPairAt(i));

        public static bool IsCloseDownXAt(this IReadOnlyList<IPriceC> This, IReadOnlyList<double> that, int i) =>
            QuantTC.X.VT(This[i - 1].Close, This[i].Close).IsDownX(that.NearPairAt(i));

        public static bool IsLowUpXAt(this IReadOnlyList<IPriceL> This, IReadOnlyList<double> that, int i) =>
            QuantTC.X.VT(This[i - 1].Low, This[i].Low).IsUpX(that.NearPairAt(i));

        public static bool IsHighDownXAt(this IReadOnlyList<IPriceH> This, IReadOnlyList<double> that, int i) =>
            QuantTC.X.VT(This[i - 1].High, This[i].High).IsDownX(that.NearPairAt(i));

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
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释