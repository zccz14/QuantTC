using System;
using System.Collections.Generic;

namespace QuantTC.Statistics.Window
{
	public partial class Project
	{
		/// <summary>
		/// Opening Run Results
		/// </summary>
		public Dictionary<IOpeningCondition, int[]> OResults;

		/// <summary>
		/// Opening-Closed Run Results
		/// </summary>
		public Dictionary<IOpeningCondition, Dictionary<IClosingCondition, Tuple<int, int>[]>> OCResults;

		/// <summary>
		/// Opening-Closed-Subject Run Results
		/// </summary>
		public Dictionary<IOpeningCondition, Dictionary<IClosingCondition, Dictionary<ISubject, object[]>>> OCSResults;

		/// <summary>
		/// Opening-Closed-Subject-Summary Run Results
		/// </summary>
		public Dictionary<IOpeningCondition,
			Dictionary<IClosingCondition, Dictionary<ISubject, Dictionary<ISummary, object>>>> OCSSResults;
	}
}