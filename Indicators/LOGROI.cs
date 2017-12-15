using System;
using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
	/// <inheritdoc />
	/// <summary>
	/// 对数回报率: LOG of Return of Investment
	/// </summary>
	public class LogROI : Indicator<double>
	{
		/// <inheritdoc />
		public LogROI(IIndicator<IPriceOC> source)
		{
			Source = source;
			Source.Update += Source_Update;
		}

		private void Source_Update()
		{
			Data.FillRange(Count, Source.Count, i => Math.Log(Source[i].Close) - Math.Log(Source[i].Open));
			FollowUp();
		}

		private IIndicator<IPriceOC> Source { get; }
	}
}