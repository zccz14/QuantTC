using System;

namespace QuantTC.Data
{
    /// <summary>
    /// Bar high low.
    /// </summary>
    public interface IBarHighLow: IBarHigh, IBarLow {}
    /// <summary>
    /// Bar open close.
    /// </summary>
    public interface IBarOpenClose : IBarOpen, IBarClose {}
    /// <summary>
    /// Bar ohc.
    /// </summary>
    public interface IBarOHC: IBarOpenClose, IBarHigh {}
    /// <summary>
    /// Bar olc.
    /// </summary>
    public interface IBarOLC: IBarOpenClose, IBarLow {}
    /// <summary>
    /// Bar ohlc.
    /// </summary>
    public interface IBarOHLC: IBarOHC, IBarOLC, IBarHighLow {}
    /// <summary>
    /// Bar open.
    /// </summary>
    public interface IBarOpen {
        /// <summary>
        /// Gets the open.
        /// </summary>
        /// <value>The open.</value>
        double Open { get; }
    }
    /// <summary>
    /// Bar close.
    /// </summary>
    public interface IBarClose {
        /// <summary>
        /// Gets the close.
        /// </summary>
        /// <value>The close.</value>
        double Close { get; }
    }
    /// <summary>
    /// Bar high.
    /// </summary>
    public interface IBarHigh {
        /// <summary>
        /// Gets the high.
        /// </summary>
        /// <value>The high.</value>
        double High { get; }
    }
    /// <summary>
    /// Bar low.
    /// </summary>
    public interface IBarLow {
        /// <summary>
        /// Gets the low.
        /// </summary>
        /// <value>The low.</value>
        double Low { get; }
    }
    /// <summary>
    /// Bar.
    /// </summary>
	public interface IBar: IBarOHLC
	{
		/// <summary>
		/// Start Datetime
		/// </summary>
		DateTime DateTime { get; }
		/// <summary>
		/// Traded Volume
		/// </summary>
		int Volume { get; }
		/// <summary>
		/// Total Position-Open Volume
		/// </summary>
		int OpenInterest { get; }
	}
    public static class IBarX {
        public static bool IsBullish(this IBarOpenClose bar) => bar.Close > bar.Open;
        public static bool IsBearish(this IBarOpenClose bar) => bar.Close < bar.Open;
		/// <summary>
		/// the Top of Real Body
		/// </summary>
		/// <param name="bar">candlestick</param>
		/// <returns></returns>
	    public static double Top(this IBarOpenClose bar) => Math.Max(bar.Close, bar.Open);
		/// <summary>
		/// the Bottom of Real Body
		/// </summary>
		/// <param name="bar">candlestick</param>
		/// <returns></returns>
	    public static double Bottom(this IBarOpenClose bar) => Math.Min(bar.Close, bar.Open);
		/// <summary>
		/// the Length of Real Body
		/// </summary>
		/// <param name="bar">candlestick</param>
		/// <returns></returns>
        public static double RealBody(this IBarOpenClose bar) => Math.Abs(bar.Close - bar.Open);
        public static double UpperShadow(this IBarOHC bar) => bar.High - bar.Top();
        public static double LowerShadow(this IBarOLC bar) => bar.Bottom() - bar.Low;
    }
}