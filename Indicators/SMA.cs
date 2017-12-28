using System.Linq;
using QuantTC.Indicators.Generic;
using static QuantTC.X;

namespace QuantTC.Indicators
{
    /// <inheritdoc />
    /// <summary>
    /// Simple Moving Average
    /// </summary>
    public class SMA : Indicator<double>
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
                i => RangeRight(0, i + 1).Take(Period).Select(ii => Source[ii]).Average());
            FollowUp();
        }

        private IIndicator<double> Source { get; }
        private int Period { get; }
    }
}