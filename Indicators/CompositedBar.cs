using System.Linq;
using QuantTC.Data;
using QuantTC.Indicators.Generic;
using static QuantTC.X;

namespace QuantTC.Indicators
{
    public class CompositedBar : Indicator<IBarPrice>
    {
        public CompositedBar(IIndicator<IBarPrice> source, int period)
        {
            Source = source;
            Period = period;
            Source.Update += Source_Update;
        }

        private void Source_Update()
        {
            for (var i = Count; i < Source.Count / Period; i++)
            {
                Data.Add(new BarPrice
                {
                    DateTime = Source[i * Period].DateTime,
                    Open = Source[i * Period].Open,
                    High = Range(i * Period, (i + 1) * Period).Max(ii => Source[ii].High),
                    Low = Range(i * Period, (i + 1) * Period).Min(ii => Source[ii].Low),
                    Close = Source[(i + 1) * Period - 1].Close,
                    Volume = Range(i * Period, (i + 1) * Period).Sum(ii => Source[ii].Volume),
                    OpenInterest = Source[(i + 1) * Period - 1].OpenInterest
                });
                FollowUp();
            }
        }

        private IIndicator<IBarPrice> Source { get; }
        public int Period { get; }
    }
    public static partial class X
    {
        public static CompositedBar Composite(this IIndicator<IBarPrice> source, int period) =>
            new CompositedBar(source, period);
    }

}