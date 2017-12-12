using System.Linq;
using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
    /// <inheritdoc />
    /// <summary>
    /// Lowest Low Value
    /// </summary>
    public class LLV : Indicator<double>
    {
        /// <inheritdoc />
        public LLV(IIndicator<IBarLow> low, int period)
        {
            Low = low;
            Period = period;
            Low.Update += Source_Update;
        }

        private void Source_Update()
        {
            Data.FillRange(Count, Low.Count, i => Functions.RangeRight(0, i + 1).Take(Period).Min(ii => Low[ii].Low));
            FollowUp();
        }

        private IIndicator<IBarLow> Low { get; }
        private int Period { get; }
    }
}