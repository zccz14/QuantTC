namespace QuantTC.Data
{
	/// <summary>
	/// Bollinger Data
	/// </summary>
	public interface IBoll
	{
		/// <summary>
		/// Lower Line
		/// </summary>
		double Lower { get; }

		/// <summary>
		/// Middle Line
		/// </summary>
		double Middle { get; }

		/// <summary>
		/// Upper Line
		/// </summary>
		double Upper { get; }
	}
	/// <summary>
	/// Extension for IBoll Datum
	/// </summary>
	public static class BollX
	{
		/// <summary>
		/// Width = Upper - Lower
		/// </summary>
		/// <param name="datum">IBoll Datum</param>
		/// <returns>Width</returns>
		public static double Width(this IBoll datum) => datum.Upper - datum.Lower;
		/// <summary>
		/// Ratio = Width / Middle
		/// </summary>
		/// <param name="datum">IBoll Datum</param>
		/// <returns>Ratio</returns>
		public static double Ratio(this IBoll datum) => datum.Width() / datum.Middle;
	}
}