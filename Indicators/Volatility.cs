using System;
using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
	/// <inheritdoc />
	/// <summary>
	/// 波动率: 对数投资回报率的标准差（按周期修正）
	/// </summary>
	public class Volatility : Indicator<double>
	{
		/// <inheritdoc />
		public Volatility(IIndicator<IBarOpenClose> source, int period)
		{
			Source = source;
			Period = period;
			LogROI = new LogROI(Source);
			LogROIStd = new StandardDeviation(LogROI, Period);
			Source.Update += Source_Update;
		}

		private void Source_Update()
		{
			Data.FillRange(Count, Source.Count / Period, i => LogROIStd[(i + 1) * Period - 1] * Math.Sqrt(Period));
			FollowUp();
		}

		private IIndicator<IBarOpenClose> Source { get; }
		private IIndicator<double> LogROI { get; }
		private IIndicator<double> LogROIStd { get; }
		/// <summary>
		/// 周期
		/// </summary>
		public int Period { get; }
	}
}