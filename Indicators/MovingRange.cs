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
    /// 移动极差
    /// </summary>
    public class MovingRange: Indicator<double>
    {
        /// <inheritdoc />
        public MovingRange(IIndicator<IPriceHL> source, int period)
        {
            Source = source;
            Period = period;
            Source.Update += SourceOnUpdate;
        }

        private void SourceOnUpdate()
        {
            Data.FillRange(Count, Source.Count,
                i => Functions.RangeRight(0, i + 1).Take(Period).Select(ii => Source[ii].High).Max() -
                     Functions.RangeRight(0, i + 1).Take(Period).Select(ii => Source[ii].Low).Min());
            FollowUp();
        }

        private IIndicator<IPriceHL> Source { get; }
        private int Period { get; }
    }
}
