using System;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
namespace QuantTC.Data
{
    public interface IPriceHL: IPriceH, IPriceL {}
    public interface IPriceOC : IPriceO, IPriceC {}
    public interface IPriceOHC: IPriceOC, IPriceH {}
    public interface IPriceOLC: IPriceOC, IPriceL {}
    public interface IPriceOHLC: IPriceOHC, IPriceOLC, IPriceHL {}
    public interface IPriceO
    {
        double Open { get; }
    }
    public interface IPriceC
    {
        double Close { get; }
    }
    public interface IPriceH {
        double High { get; }
    }
    public interface IPriceL {
        double Low { get; }
    }
	public interface IPrice: IPriceOHLC
	{
		DateTime DateTime { get; }
		int Volume { get; }
		int OpenInterest { get; }
	}
    public static class X {
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
    }
}
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
