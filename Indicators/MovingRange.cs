using System.Linq;
using QuantTC.Data;
using QuantTC.Indicators.Generic;
using static QuantTC.X;

namespace QuantTC.Indicators
{
    /// <inheritdoc />
    /// <summary>
    /// 移动极差
    /// </summary>
    public class MovingRange : Indicator<double>
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
                i => RangeRight(0, i + 1).Take(Period).Select(ii => Source[ii].High).Max() -
                     RangeRight(0, i + 1).Take(Period).Select(ii => Source[ii].Low).Min());
            FollowUp();
        }

        private IIndicator<IPriceHL> Source { get; }
        private int Period { get; }
    }
}