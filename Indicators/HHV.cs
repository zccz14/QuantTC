using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
    /// <inheritdoc />
    /// <summary>
    /// Highest High Value
    /// </summary>
    public class HHV: Indicator<double>
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
            Data.FillRange(Count, High.Count, i => Functions.RangeRight(0, i + 1).Take(Period).Max(ii => High[ii].High));
            FollowUp();
        }

        private IIndicator<IPriceH> High { get; }
        private int Period { get; }
    }
}
