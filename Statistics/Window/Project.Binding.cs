using System;
using System.Collections.Generic;

namespace QuantTC.Statistics.Window
{
	public partial class Project
	{
		public void Bind(IOpeningCondition oc)
		{
			if (!Os.Contains(oc))
			{
				Os.Add(oc);
			}
		}
		public void Bind(IOpeningCondition oc, IClosingCondition cc)
		{
			Bind(oc);
			if (OToCs.ContainsKey(oc) == false)
			{
				OToCs.Add(oc, new List<IClosingCondition>());
			}
			if (!OToCs[oc].Contains(cc))
			{
				OToCs[oc].Add(cc);
			}
		}

		public void Bind(IOpeningCondition oc, IClosingCondition cc, ISubject window)
		{
			Bind(oc, cc);
			var key = Tuple.Create(oc, cc);
			if (OCToSs.ContainsKey(key) == false)
			{
				OCToSs.Add(key, new List<ISubject>());
			}
			if (!OCToSs[key].Contains(window))
			{
				OCToSs[key].Add(window);
			}
		}

		public void Bind(IOpeningCondition oc, IClosingCondition cc, ISubject window, ISummary summary)
		{
			Bind(oc, cc, window);
			var key = Tuple.Create(oc, cc, window);
			if (OCSToSs.ContainsKey(key) == false)
			{
				OCSToSs.Add(key, new List<ISummary>());
			}
			if (!OCSToSs[key].Contains(summary))
			{
				OCSToSs[key].Add(summary);
			}
		}
	}
}