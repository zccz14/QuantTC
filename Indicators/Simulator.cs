using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QuantTC.Data;
using QuantTC.Indicators.Generic;

namespace QuantTC.Indicators
{
    public class Simulator: IIndicator<IBar>
    {
	    public Simulator(Dumb<IBar> source)
	    {
		    Source = source;
		    Source.Update += Resume;
	    }

	    public event Action Update;
	    public IEnumerator<IBar> GetEnumerator() => Source.Take(Count).GetEnumerator();

	    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	    public int Count { get; private set; }

	    public IBar this[int index] => Source[index];

	    private Dumb<IBar> Source { get; }

	    public void Replay()
	    {
		    Count = 0;
		    Resume();
	    }

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
