using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
	/// <inheritdoc />
    public class Simulator: IIndicator<IBar>
    {
	    /// <inheritdoc />
	    public Simulator(IIndicator<IBar> source)
	    {
		    Source = source;
		    Source.Update += Resume;
	    }
		/// <inheritdoc />
	    public event Action Update;
	    /// <inheritdoc />
	    public IEnumerator<IBar> GetEnumerator() => Source.Take(Count).GetEnumerator();

	    /// <inheritdoc />
	    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	    /// <inheritdoc />
	    public int Count { get; private set; }

	    /// <inheritdoc />
	    public IBar this[int index] => Source[index];

	    private IIndicator<IBar> Source { get; }

		/// <summary>
		/// Replay Source
		/// </summary>
	    public void Replay()
	    {
		    Count = 0;
		    Resume();
	    }
		/// <summary>
		/// Resume (Continue when source updated)
		/// </summary>
	    public void Resume()
	    {
		    Functions.Range(Count, Source.Count).ForEach(i =>
		    {
			    Count = i + 1;
			    Update?.Invoke();
		    });
	    }
    }
}
