using System;
using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
    /// <summary>
    /// Bollinger Bands
    /// </summary>
    public class BOLL : Indicator<IBoll>
    {
        /// <summary>
        /// Recommanded Constructor
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="period">Period</param>
        /// <param name="deviation">Deviation</param>
        public BOLL(IIndicator<double> source, int period, double deviation)
        {
            Source = source;
            Period = period;
            Deviation = deviation;
            Middle = Source.SMA(Period);
            Std = Source.StdDev(Period, Middle);
            Upper = new BinaryOperation<double, double, double>(Middle, Std, (m, s) => m + Deviation * s);
            Lower = new BinaryOperation<double, double, double>(Middle, Std, (m, s) => m - Deviation * s);
            Upper.Update += Main;
            Middle.Update += Main;
            Lower.Update += Main;
        }

        private void Main()
        {
            Data.FillRange(Count, X.Min(Lower.Count, Middle.Count, Upper.Count),
                i => new Datum {Lower = Lower[i], Middle = Middle[i], Upper = Upper[i]});
            FollowUp();
        }

        private class Datum : IBoll
        {
            /// <inheritdoc />
            public double Upper { get; set; }

            /// <inheritdoc />
            public double Middle { get; set; }

            /// <inheritdoc />
            public double Lower { get; set; }
        }

        private IIndicator<double> Source { get; }

        /// <summary>
        /// Upper Line
        /// </summary>
        public IIndicator<double> Upper { get; }

        /// <summary>
        /// Middle Line
        /// </summary>
        public IIndicator<double> Middle { get; }

        /// <summary>
        /// Lower Line
        /// </summary>
        public IIndicator<double> Lower { get; }

        /// <summary>
        /// Moving Standard Deviation
        /// </summary>
        public IIndicator<double> Std { get; }

        /// <summary>
        /// Period
        /// </summary>
        public int Period { get; }

        /// <summary>
        /// Deviation
        /// </summary>
        public double Deviation { get; }
    }
}