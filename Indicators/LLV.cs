using System;
using System.Linq;
using QuantTC.Data;
using QuantTC.Indicators.Generic;
using static QuantTC.X;

namespace QuantTC.Indicators
{
    /// <inheritdoc />
    /// <summary>
    /// Lowest Low Value
    /// </summary>
    [Obsolete("Using MovingMostValue instand")]
    public class LLV : Indicator<double>
    {
        /// <inheritdoc />
        public LLV(IIndicator<IPriceL> low, int period)
        {
            Low = low;
            Period = period;
            Low.Update += Source_Update;
        }

        private void Source_Update()
        {
            Data.FillRange(Count, Low.Count, i => RangeR(0, i + 1).Take(Period).Min(ii => Low[ii].Low));
            FollowUp();
        }

        private IIndicator<IPriceL> Low { get; }
        private int Period { get; }
    }
}