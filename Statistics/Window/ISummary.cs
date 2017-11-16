namespace QuantTC.Statistics.Window
{
	/// <inheritdoc />
	public interface ISummary : ITitle
	{
		/// <summary>
		/// Get into summary
		/// </summary>
		/// <param name="objects"></param>
		/// <returns>result</returns>
		object GetResult(params object[] objects);
	}
}