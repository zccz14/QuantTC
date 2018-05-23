using System;

namespace QuantTC.Data
{
    /// <inheritdoc cref="IBarPrice"/>
    public class BarPrice : IBarPrice
    {
        ///	<inheritdoc />
        public DateTime DateTime { get; set; }

        ///	<inheritdoc />
        public double Price => Close;

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
        public static BarPrice FromStringArray(string[] data) => new BarPrice
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
        [Obsolete("Using Parse instand")]
        public static BarPrice FromString(string data) => FromStringArray(data.Split(','));

        /// <summary>
        /// Converts the string representation of {Datetime},{Open},{High},{Low},{Close},{Volume},{OpenInterest} to its Price equivalent.
        /// </summary>
        /// <param name="s">A string contains a price to convert</param>
        /// <returns>Price</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static BarPrice Parse(string s) => FromStringArray(s.Split(','));
    }
}