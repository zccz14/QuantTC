using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
	/// <summary>
	/// Bollinger Bands
	/// </summary>
	public class BOLL
	{
		/// <summary>
		/// Recommanded Constructor
		/// </summary>
		/// <param name="source">Source</param>
		/// <param name="period">Period</param>
		/// <param name="deviation">Deviation</param>
		public BOLL(IIndicator<double> source, int period, double deviation)
		{
			Source = source;
			Period = period;
			Deviation = deviation;
			Middle = Source.SMA(Period);
			Std = Source.StdDev(Period, Middle);
			Upper = new BinaryOperation<double, double, double>(Middle, Std, (m, s) => m + Deviation * s);
			Lower = new BinaryOperation<double, double, double>(Middle, Std, (m, s) => m - Deviation * s);
		}

		private IIndicator<double> Source { get; }

		/// <summary>
		/// Upper Line
		/// </summary>
		public IIndicator<double> Upper { get; }

		/// <summary>
		/// Middle Line
		/// </summary>
		public IIndicator<double> Middle { get; }

		/// <summary>
		/// Lower Line
		/// </summary>
		public IIndicator<double> Lower { get; }

		/// <summary>
		/// Moving Standard Deviation
		/// </summary>
		public IIndicator<double> Std { get; }

		/// <summary>
		/// Period
		/// </summary>
		public int Period { get; }

		/// <summary>
		/// Deviation
		/// </summary>
		public double Deviation { get; }
	}
}
