using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
	/// <inheritdoc cref="Indicator{T}" />
	/// <summary>
	/// Load Price From CSV File
	/// 
	/// [[Datetime, Open, High, Low, Close, Volume, OpenInterest]]
	/// </summary>
	public sealed class CsvBarList: Indicator<IPrice>, IDisposable
    {
	    /// <inheritdoc />
	    public CsvBarList(string path, bool header = true)
	    {
		    Data.AddRange(File.ReadAllLines(path).Skip(header ? 1 : 0).Select(Datum.FromString));
	    }

	    private class Datum : IPrice
	    {
		    public DateTime DateTime { get; private set; }
		    public double Open { get; private set; }
		    public double High { get; private set; }
		    public double Low { get; private set; }
		    public double Close { get; private set; }
		    public int Volume { get; private set; }
		    public int OpenInterest { get; private set; }

		    private static Datum FromStringArray(IReadOnlyList<string> data) => new Datum
		    {
			    DateTime = DateTime.Parse(data[0]),
			    Open = double.Parse(data[1]),
			    High = double.Parse(data[2]),
			    Low = double.Parse(data[3]),
			    Close = double.Parse(data[4]),
			    Volume = int.Parse(data[5]),
			    OpenInterest = int.Parse(data[6])
		    };

		    public static Datum FromString(string data) => FromStringArray(data.Split(','));
	    }
		/// <inheritdoc />
		/// <summary>
		/// Call Follow Up
		/// </summary>
	    public void Dispose()
	    {
		    FollowUp();
	    }
    }
}
