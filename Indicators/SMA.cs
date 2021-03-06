﻿using System.Linq;
using QuantTC.Indicators.Generic;
using static QuantTC.X;

namespace QuantTC.Indicators
{
    /// <inheritdoc cref="IMovingAverage" />
    /// <summary>
    /// Simple Moving Average
    /// </summary>
    public class SMA : Indicator<double>, IMovingAverage
    {
        /// <inheritdoc />
        public SMA(IIndicator<double> source, int period)
        {
            Source = source;
            Period = period;
            Source.Update += Source_Update;
        }

        private void Source_Update()
        {
            Data.FillRange(Count, Source.Count,
                i => RangeR(0, i + 1).Take(Period).Select(ii => Source[ii]).Average());
            FollowUp();
        }

        private IIndicator<double> Source { get; }
        private int Period { get; }
    }
    public static partial class X
    {
        public static SMA SMA(this IIndicator<double> source, int period) => new SMA(source, period);
    }

}