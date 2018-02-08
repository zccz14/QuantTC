using QuantTC.Data;
using QuantTC.Indicators.Generic;

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
            High = Source.Transform(p => p.High);
            Low = Source.Transform(p => p.Low);
            Hhv = High.HighestValue(Period);
            Llv = Low.LowestValue(Period);
            Source.Update += SourceOnUpdate;
        }

        private void SourceOnUpdate()
        {
            Data.FillRange(Count, Source.Count, i => Hhv[i] - Llv[i]);
            FollowUp();
        }

        private IIndicator<IPriceHL> Source { get; }
        private IIndicator<double> High { get; }
        private IIndicator<double> Low { get; }
        private MovingMostValue<double> Hhv { get; }
        private MovingMostValue<double> Llv { get; }
        private int Period { get; }
    }
}