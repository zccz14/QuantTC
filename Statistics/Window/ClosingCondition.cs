using System;

namespace QuantTC.Statistics.Window
{
	/// <inheritdoc />
	public class ClosingCondition: IClosingCondition
	{
		/// <inheritdoc />
		public ClosingCondition(string title, Func<int, int, bool> predicate)
		{
			Title = title;
			Predicate = predicate;
		}

		/// <inheritdoc />
		public string Title { get; }

		/// <inheritdoc />
		public bool Query(int start, int end) => Predicate(start, end);

		/// <inheritdoc />
		public override string ToString() => Title;

		private Func<int, int, bool> Predicate { get; }
	}
}