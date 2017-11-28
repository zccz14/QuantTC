using System.Linq;
using QuantTC.Data;
using QuantTC.Indicators.Generic;
using static QuantTC.Functions;

namespace QuantTC.Indicators
{
	public class CompositedBar : Indicator<IBar>
	{
		public CompositedBar(IIndicator<IBar> source, int period)
		{
			Source = source;
			Period = period;
			Source.Update += Source_Update;
		}

		private void Source_Update()
		{
			for (var i = Count; i < Source.Count / Period; i++)
			{
				Data.Add(new Bar
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

		private IIndicator<IBar> Source { get; }
		public int Period { get; }
	}
}