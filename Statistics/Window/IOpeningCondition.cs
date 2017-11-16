namespace QuantTC.Statistics.Window
{
	/// <inheritdoc />
	public interface IOpeningCondition : ITitle
	{
		/// <summary>
		/// Should Open Window at [index]?
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		bool Query(int index);
	}
}