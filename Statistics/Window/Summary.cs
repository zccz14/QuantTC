using System;

namespace QuantTC.Statistics.Window
{
	/// <inheritdoc />
	public class Summary : ISummary
	{
		/// <inheritdoc />
		public Summary(string title, Func<object[], object> func)
		{
			Title = title;
			Func = func;
		}

		/// <inheritdoc />
		public string Title { get; }

		/// <inheritdoc />
		public override string ToString() => Title;

		/// <inheritdoc />
		public object GetResult(params object[] objects) => Func(objects);

		private Func<object[], object> Func { get; }
	}
}