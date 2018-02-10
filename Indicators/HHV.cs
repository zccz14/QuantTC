using System;
using System.Linq;
using QuantTC.Data;
using QuantTC.Indicators.Generic;
using static QuantTC.X;

namespace QuantTC.Indicators
{
    /// <inheritdoc />
    /// <summary>
    /// Highest High Value
    /// </summary>
    [Obsolete("Using MovingMostValue instand")]
    public class HHV : Indicator<double>
    {
        /// <inheritdoc />
        public HHV(IIndicator<IPriceH> high, int period)
        {
            High = high;
            Period = period;
            High.Update += Source_Update;
        }

        private void Source_Update()
        {
            Data.FillRange(Count, High.Count, i => RangeR(0, i + 1).Take(Period).Max(ii => High[ii].High));
            FollowUp();
        }

        private IIndicator<IPriceH> High { get; }
        private int Period { get; }
    }
}