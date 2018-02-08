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

        public IIndicator<IPriceHL> Source { get; }
        public IIndicator<double> High { get; }
        public IIndicator<double> Low { get; }
        public MovingMostValue<double> Hhv { get; }
        public MovingMostValue<double> Llv { get; }
        public int Period { get; }
    }
}