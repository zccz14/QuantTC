using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
    /// <summary>
    /// Indicator Creator Extension: Help creating indicator more convenient
    /// </summary>
    public static class IndicatorCreator
    {
        public static SMA SMA(this IIndicator<double> source, int period) => new SMA(source, period);
        public static EMA EMA(this IIndicator<double> source, int period) => new EMA(source, period);

        public static BOLL BOLL(this IIndicator<double> source, int period, double deviation) =>
            new BOLL(source, period, deviation);

        public static MACD MACD(this IIndicator<double> source, int fastPeriod, int slowPeriod, int diffPeriod) =>
            new MACD(source, fastPeriod, slowPeriod, diffPeriod);

        public static StandardDeviation StdDev(this IIndicator<double> source, int period) =>
            new StandardDeviation(source, period);

        public static StandardDeviation
            StdDev(this IIndicator<double> source, int period, IIndicator<double> average) =>
            new StandardDeviation(source, average, period);

        public static CompositedBar Composite(this IIndicator<IPrice> source, int period) =>
            new CompositedBar(source, period);
    }
}