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
		/// Width / Middle
		/// </summary>
		double Ratio { get; }

		/// <summary>
		/// Standard Deviation
		/// </summary>
		double Std { get; }

		/// <summary>
		/// Upper Line
		/// </summary>
		double Upper { get; }

		/// <summary>
		/// Upper - Lower
		/// </summary>
		double Width { get; }
	}
}