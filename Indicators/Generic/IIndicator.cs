using System;
using System.Collections.Generic;

namespace QuantTC.Indicators.Generic
{
	/// <summary>
	/// Technical Indicators are several IReadOnlyList with an update event
	/// </summary>
	public interface IIndicator
	{
		/// <summary>
		/// fired every time the indicator was updated
		/// </summary>
		event Action Update;
	}

    /// <inheritdoc cref="IIndicator" />
	/// <inheritdoc cref="IReadOnlyList{T}" />
	/// <summary>
	/// IIndicator as a IReadOnlyList&lt;T&gt;
	/// </summary>
	/// <typeparam name="T">Data Type</typeparam>
	public interface IIndicator<out T> : IIndicator, IReadOnlyList<T>
	{
	}
}
