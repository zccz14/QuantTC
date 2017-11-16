using System;
using System.Collections.Generic;
using T1 = QuantTC.Statistics.Window.IOpeningCondition;
using T2 = QuantTC.Statistics.Window.IClosingCondition;

namespace QuantTC.Statistics.Window
{
	using T3 = Tuple<T1, T2>;
	using T4 = Tuple<T1, T2, ISubject>;

	public partial class Project
	{
		/// <summary>
		/// Open Cond Collections
		/// </summary>
		private ICollection<T1> Os { get; } = new List<T1>();

		/// <summary>
		/// Close Cond Collection grouped by Open Cond
		/// </summary>
		private Dictionary<T1, ICollection<T2>> OToCs { get; } = new Dictionary<T1, ICollection<T2>>();

		/// <summary>
		/// Subject Collection grouped by Open Cond and Close Cond
		/// </summary>
		private Dictionary<T3, ICollection<ISubject>> OCToSs { get; } = new Dictionary<T3, ICollection<ISubject>>();

		/// <summary>
		/// Summary Collection grouped by Open Cond, Close Cond and Subject
		/// </summary>
		private Dictionary<T4, ICollection<ISummary>> OCSToSs { get; } = new Dictionary<T4, ICollection<ISummary>>();
	}
}