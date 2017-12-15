using System;

namespace QuantTC.Data
{
	/// <inheritdoc cref="IPrice"/>
	public class Price : IPrice
	{
		///	<inheritdoc />
		public DateTime DateTime { get; set; }

		///	<inheritdoc />
		public double Open { get; set; }

		///	<inheritdoc />
		public double High { get; set; }

		///	<inheritdoc />
		public double Low { get; set; }

		///	<inheritdoc />
		public double Close { get; set; }

		///	<inheritdoc />
		public int Volume { get; set; }

		///	<inheritdoc />
		public int OpenInterest { get; set; }

		/// <summary>
		/// Construct a price instance from string array
		/// </summary>
		/// <param name="data">[Datetime, Open, High, Low, Close, Volume, OpenInterest]</param>
		public static Price FromStringArray(string[] data) => new Price
		{
			DateTime = DateTime.Parse(data[0]),
			Open = double.Parse(data[1]),
			High = double.Parse(data[2]),
			Low = double.Parse(data[3]),
			Close = double.Parse(data[4]),
			Volume = int.Parse(data[5]),
			OpenInterest = int.Parse(data[6])
		};

		/// <summary>
		/// Construct a price instance from a formatted string
		/// </summary>
		/// <param name="data">{Datetime},{Open},{High},{Low},{Close},{Volume},{OpenInterest}</param>
		public static Price FromString(string data) => FromStringArray(data.Split(','));
	}
}