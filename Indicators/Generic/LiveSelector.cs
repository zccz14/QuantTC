using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QuantTC.Indicators.Generic
{
	/// <inheritdoc />
	/// <summary>
	/// Lively Select (Map) an indicator's data to another without copy its data (no cache)
	/// </summary>
	/// <typeparam name="T1">Origin Type</typeparam>
	/// <typeparam name="T2">Target Type</typeparam>
	/// <remarks>It calls selector function every time accessing its data, which may cause performance issue</remarks>
    public class LiveSelector<T1, T2>: Indicator, IIndicator<T2>
    {
	    /// <inheritdoc />
		public LiveSelector(IIndicator<T1> source, Func<T1, int, T2> selector)
	    {
		    Source = source;
		    Selector = selector;
		    Source.Update += FollowUp;
	    }

	    /// <inheritdoc />
	    public IEnumerator<T2> GetEnumerator() => Source.Select(Selector).GetEnumerator();

	    /// <inheritdoc />
	    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	    /// <inheritdoc />
	    public int Count => Source.Count;

	    /// <inheritdoc />
	    public T2 this[int index] => Selector(Source[index], index);

		private IIndicator<T1> Source { get; }
		private Func<T1, int, T2> Selector { get; }
    }
}
